using UnityEngine;
using QFramework;
using System.Collections.Generic;

namespace QFramework.PG
{
	public partial class GlobalAudioPlay : ViewController
	{
		//单例
		public static GlobalAudioPlay Instance { get; private set; }
		private void Awake() {
			Instance = this;
		}
		/// <summary>
		/// 音频列表
		/// </summary>
		private Dictionary<string, AudioClip> AudioClips = new Dictionary<string, AudioClip>();
		public void PlayerAudioSourceByPath(string name, bool loop = false)
		{
			if(AudioClips.ContainsKey(name))
			{
				SelfAudioSource.clip = AudioClips[name];
				SelfAudioSource.loop = loop;
				SelfAudioSource.Play();
			}
			else
			{
				AudioClips[name] = Resources.Load<AudioClip>(Config.GunClipPath + name);
				if(AudioClips[name] == null)
				{
					Debug.LogError("AudioClip not found: " + name);
					return;
				}
				PlayerAudioSourceByPath(name, loop);
			}
		}

		public void PlayerAudioSourceByClip(AudioClip clip, bool loop = false)
		{
			SelfAudioSource.clip = clip;
			SelfAudioSource.loop = loop;
			SelfAudioSource.Play();
		}
	}
}
