using UnityEngine;
using QFramework;
using System.Collections.Generic;
using System;
using System.Collections;

namespace QFramework.PG
{
	public partial class GlobalAudioPlay : ViewController
	{
		//单例
		public static GlobalAudioPlay Instance { get; private set; }
		private Coroutine mWaitPlayCompleteCoroutine;
		//播放令牌
		private int mPlayToken;
		private void Awake() {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}


		/// <summary>
		/// 音频列表
		/// </summary>
		private Dictionary<string, AudioClip> AudioClips = new Dictionary<string, AudioClip>();
		public void PlayerAudioSourceByPath(string name, bool loop = false, Action onComplete = null)
		{
			if (SelfAudioSource.isPlaying) return;

			if(AudioClips.ContainsKey(name))
			{
				SelfAudioSource.clip = AudioClips[name];
				SelfAudioSource.loop = loop;
				SelfAudioSource.Play();
				StartWaitPlayComplete(loop, onComplete);
			}
			else
			{
				AudioClips[name] = Resources.Load<AudioClip>(Config.GunClipPath + name);
				if(AudioClips[name] == null)
				{
					Debug.LogError("AudioClip not found: " + name);
					return;
				}
				PlayerAudioSourceByPath(name, loop, onComplete);
			}
		}

		/// <summary>
		/// 播放音频，若当前正在播放则丢弃新请求。
		/// </summary>
		/// <param name="clip">音频</param>
		/// <param name="loop">是否循环</param>
		/// <param name="onComplete">完成回调</param>
		public void PlayerAudioSourceByClip(AudioClip clip, bool loop = false, Action onComplete = null)
		{
			if (SelfAudioSource.isPlaying) return;

			if(clip == null)
			{
				Debug.LogError("AudioClip is null.");
				return;
			}
			SelfAudioSource.clip = clip;
			SelfAudioSource.loop = loop;
			SelfAudioSource.Play();
			StartWaitPlayComplete(loop, onComplete);
		}

		/// <summary>
		/// 叠加播放短音效，不替换当前主 clip（适合受击、拾取等）。
		/// 请保证 GlobalAudioPlay 上 SelfAudioSource 的 Spatial Blend = 0（2D），否则仍会随距离变轻。
		/// </summary>
		public void PlayOneShot(AudioClip clip, float volumeScale = 1f)
		{
			if (clip == null || SelfAudioSource == null) return;
			SelfAudioSource.PlayOneShot(clip, volumeScale);
		}

		/// <summary>
		/// 开始等待音频播放完成
		/// </summary>
		/// <param name="loop">是否循环</param>
		/// <param name="onComplete">完成回调</param>
		private void StartWaitPlayComplete(bool loop, Action onComplete)
		{
			mPlayToken++;
			if(mWaitPlayCompleteCoroutine != null)
			{
				StopCoroutine(mWaitPlayCompleteCoroutine);
				mWaitPlayCompleteCoroutine = null;
			}

			if(loop || onComplete == null)
			{
				return;
			}

			int currentToken = mPlayToken;
			mWaitPlayCompleteCoroutine = StartCoroutine(WaitPlayComplete(currentToken, onComplete));
		}

		/// <summary>
		/// 等待音频播放完成
		/// </summary>
		/// <param name="playToken">播放令牌</param>
		/// <param name="onComplete">完成回调</param>
		private IEnumerator WaitPlayComplete(int playToken, Action onComplete)
		{
			while(playToken == mPlayToken && SelfAudioSource != null && SelfAudioSource.isPlaying)
			{
				yield return null;
			}

			if(playToken == mPlayToken)
			{
				mWaitPlayCompleteCoroutine = null;
				onComplete?.Invoke();
			}
		}

	}
}
