using UnityEngine.Serialization;
using Game.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Gameplay
{
    /// <summary>
    /// 常驻 BGM 管理器, 根据当前场景自动切换音乐, 以及响应玩家死亡事件播放死亡音乐.
    /// </summary> <summary>
    /// 
    /// </summary>
    public class BgmAudioPlay : MonoBehaviour
    {
        private const string StartSceneName = "StartScene";
        private const string GameSceneName = "GameScene";

        // 资源通过短名预加载, 业务读取已就绪的缓存.

        [SerializeField, AddressableKey(AddressableAssetKind.AudioClip)] public string startSceneBgmKey = string.Empty;

        public AudioClip startSceneBgm => AddressableAssetAccess.Get<AudioClip>(startSceneBgmKey);
        // 资源通过短名预加载, 业务读取已就绪的缓存.
        [SerializeField, AddressableKey(AddressableAssetKind.AudioClip)] public string mainSceneBgmKey = string.Empty;
        public AudioClip mainSceneBgm => AddressableAssetAccess.Get<AudioClip>(mainSceneBgmKey);
        // 资源通过短名预加载, 业务读取已就绪的缓存.
        [SerializeField, AddressableKey(AddressableAssetKind.AudioClip)] public string playerDeadBgmKey = string.Empty;
        public AudioClip playerDeadBgm => AddressableAssetAccess.Get<AudioClip>(playerDeadBgmKey);

        public static BgmAudioPlay Instance { get; private set; }

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        [SerializeField] private AudioSource m_audioSource;

        public void PlayStartSceneBgm()
        {
            PlayBgm(startSceneBgm, true);
        }

        public void PlayMainSceneBgm()
        {
            PlayBgm(mainSceneBgm, true);
        }

        public void PlayPlayerDeadBgm()
        {
            PlayBgm(playerDeadBgm, false);
        }

        void OnEnable()
        {
            if (Instance != this) return;

            // 监听场景激活变化, 让常驻 BGM 管理器根据当前场景切换音乐.
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
            EventCenter.AddListener(CoreEvents.PlayerDied, PlayPlayerDeadBgm);
            PlayBgmByScene(SceneManager.GetActiveScene().name);
        }

        void OnDisable()
        {
            if (Instance != this) return;

            SceneManager.activeSceneChanged -= OnActiveSceneChanged;
            EventCenter.RemoveListener(CoreEvents.PlayerDied, PlayPlayerDeadBgm);
        }

        private void OnActiveSceneChanged(Scene previousScene, Scene nextScene)
        {
            PlayBgmByScene(nextScene.name);
        }

        private void PlayBgmByScene(string sceneName)
        {
            switch (sceneName)
            {
                case StartSceneName:
                    PlayStartSceneBgm();
                    break;
                case GameSceneName:
                    PlayMainSceneBgm();
                    break;
            }
        }

        private void PlayBgm(AudioClip clip, bool loop)
        {
            // 相同音乐正在播放时不重启, 避免重复切场景事件导致 BGM 从头播放.
            if (m_audioSource.clip == clip && m_audioSource.isPlaying && m_audioSource.loop == loop) return;

            m_audioSource.clip = clip;
            m_audioSource.loop = loop;
            m_audioSource.Play();
        }
    }
}
