using UnityEngine;
using System.Collections.Generic;

public class AudioPlay : MonoBehaviour {
    [Header("音源播放源")]
    [SerializeField] private AudioSource source;

    [Header("音频列表")]
    [SerializeField] private List<AudioClip> clips;

    [Header("当前播放的音源")]
    [SerializeField] private AudioClip currentClip;
    [SerializeField] private int index = 0;
    AudioClip randomClip => clips[UnityEngine.Random.Range(0, clips.Count)];


    [Header("音源类型")]
    [SerializeField] private bool isSFX = true;

    [Header("是否循环播放")]
    [SerializeField] private bool loop = false;
    [Header("随机播放")]
    [SerializeField] private bool isRandom = true;

    [Header("声音范围")]
    [SerializeField] private float min_distance = 1f;
    [SerializeField] private float max_distance = 20f;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.reverbZoneMix = isSFX ? 1 : 0;
        source.minDistance = min_distance;
        source.maxDistance = max_distance;
    }

    /// <summary>
    /// 播放音频
    /// </summary>
    /// <param name="isRandom">是否随机播放,sfx推荐为true,bgm推荐是false(设置为false会顺序播放)</param>
    public void Play()
    {
        // 没有可播放音频或音源时,直接返回.
        if (source == null || clips == null || clips.Count == 0) return;

        // SFX 正在播放时不打断,BGM 允许打断当前播放.
        if (isSFX && source.isPlaying) return;

        currentClip = isRandom ? randomClip : clips[index];

        // 顺序播放时,播放后切到下一个索引.
        if (!isRandom) index = (index + 1) % clips.Count;

        source.clip = currentClip;
        source.loop = loop; 
        source.Play();

    }
}
