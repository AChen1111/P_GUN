using System.Collections.Generic;
using Game.UI.Settings;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Game.UI
{
    public partial class SettingsPanel : UIPanelBase
    {
        private readonly int[] frameRateOptions = { 30, 60, 120, 144, -1 };
        private readonly List<Resolution> availableResolutions = new List<Resolution>();

        [Header("音频配置")]
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private string masterVolumeParameter = "Master";
        [SerializeField] private string musicVolumeParameter = "BGM";
        [SerializeField] private string sfxVolumeParameter = "SFX";

        private GameSettingsData currentData;
        private bool isRefreshingView;

        /// <summary>
        /// 初始化运行时依赖.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            GetBindComponents(gameObject);
            BuildDropdownOptions();

            void BuildDropdownOptions()
            {
                availableResolutions.Clear();
                m_Drop_Resolution.ClearOptions();
                List<string> resolutionOptions = new List<string>();
                Resolution[] resolutions = Screen.resolutions;
                for (int i = 0; i < resolutions.Length; i++)
                {
                    Resolution resolution = resolutions[i];
                    availableResolutions.Add(resolution);
                    resolutionOptions.Add($"{resolution.width} x {resolution.height} @ {GetRefreshRateHz(resolution)}Hz");
                }

                m_Drop_Resolution.AddOptions(resolutionOptions);
                m_Drop_FrameRate.ClearOptions();
                m_Drop_FrameRate.AddOptions(new List<string> { "30", "60", "120", "144", "不限制" });
            }
}

        /// <summary>
        /// 注册启用时需要的监听.
        /// </summary>
        private void OnEnable()
        {
            m_Tog_Display.onValueChanged.AddListener(OnDisplayTabChanged);
            m_Tog_Audio.onValueChanged.AddListener(OnAudioTabChanged);
            m_Drop_Resolution.onValueChanged.AddListener(OnResolutionChanged);
            m_Tog_FullScreen.onValueChanged.AddListener(OnFullScreenChanged);
            m_Tog_VSync.onValueChanged.AddListener(OnVSyncChanged);
            m_Drop_FrameRate.onValueChanged.AddListener(OnFrameRateChanged);
            m_Slider_MasterVolume.onValueChanged.AddListener(OnMasterVolumeChanged);
            m_Slider_MusicVolume.onValueChanged.AddListener(OnMusicVolumeChanged);
            m_Slider_SfxVolume.onValueChanged.AddListener(OnSfxVolumeChanged);
            m_Btn_Apply.onClick.AddListener(ApplyAndSave);
            m_Btn_Back.onClick.AddListener(CloseByStack);
        }

        /// <summary>
        /// 注销禁用时需要的监听.
        /// </summary>
        private void OnDisable()
        {
            m_Tog_Display.onValueChanged.RemoveListener(OnDisplayTabChanged);
            m_Tog_Audio.onValueChanged.RemoveListener(OnAudioTabChanged);
            m_Drop_Resolution.onValueChanged.RemoveListener(OnResolutionChanged);
            m_Tog_FullScreen.onValueChanged.RemoveListener(OnFullScreenChanged);
            m_Tog_VSync.onValueChanged.RemoveListener(OnVSyncChanged);
            m_Drop_FrameRate.onValueChanged.RemoveListener(OnFrameRateChanged);
            m_Slider_MasterVolume.onValueChanged.RemoveListener(OnMasterVolumeChanged);
            m_Slider_MusicVolume.onValueChanged.RemoveListener(OnMusicVolumeChanged);
            m_Slider_SfxVolume.onValueChanged.RemoveListener(OnSfxVolumeChanged);
            m_Btn_Apply.onClick.RemoveListener(ApplyAndSave);
            m_Btn_Back.onClick.RemoveListener(CloseByStack);
        }
        protected override void OnOpen()
        {
            currentData = GameSettingsStore.Load();
            RefreshView();
            ShowPage(SettingsPage.Display);

            void RefreshView()
            {
                isRefreshingView = true;
                m_Drop_Resolution.value = FindResolutionIndex(currentData);
                m_Tog_FullScreen.isOn = currentData.FullScreen;
                m_Tog_VSync.isOn = currentData.VSync;
                m_Drop_FrameRate.value = FindFrameRateIndex(currentData.FrameRateLimit);
                m_Slider_MasterVolume.value = currentData.MasterVolume;
                m_Slider_MusicVolume.value = currentData.MusicVolume;
                m_Slider_SfxVolume.value = currentData.SfxVolume;
                isRefreshingView = false;
            }

    int FindFrameRateIndex(int frameRateLimit)
    {
        for (int i = 0; i < frameRateOptions.Length; i++)
        {
            if (frameRateOptions[i] == frameRateLimit)
            {
                return i;
            }
        }

        return 1;
    }

    int FindResolutionIndex(GameSettingsData data)
    {
        for (int i = 0; i < availableResolutions.Count; i++)
        {
            Resolution resolution = availableResolutions[i];
            if (resolution.width == data.ResolutionWidth && resolution.height == data.ResolutionHeight && GetRefreshRateHz(resolution) == data.RefreshRate)
            {
                return i;
            }
        }

        return Mathf.Max(0, availableResolutions.Count - 1);
    }
}
        private void ShowPage(SettingsPage page)
        {
            // 分页只切换内容容器, 不参与UIStackManager的面板栈.
            m_Rect_DisplayPage.gameObject.SetActive(page == SettingsPage.Display);
            m_Rect_AudioPage.gameObject.SetActive(page == SettingsPage.Audio);

            isRefreshingView = true;
            m_Tog_Display.isOn = page == SettingsPage.Display;
            m_Tog_Audio.isOn = page == SettingsPage.Audio;
            isRefreshingView = false;
        }
        private void ApplyAndSave()
        {
            GameSettingsStore.Save(currentData);
            GameSettingsStore.Apply(currentData, audioMixer, masterVolumeParameter, musicVolumeParameter, sfxVolumeParameter);
            CloseByStack();
        }
        private void CloseByStack()
        {
            UIStackManager stackManager = UIStackManager.Instance;
            if (stackManager != null)
            {
                stackManager.Pop();
            }
        }
        private void OnDisplayTabChanged(bool isOn)
        {
            if (isRefreshingView || !isOn)
            {
                return;
            }

            ShowPage(SettingsPage.Display);
        }
        private void OnAudioTabChanged(bool isOn)
        {
            if (isRefreshingView || !isOn)
            {
                return;
            }

            ShowPage(SettingsPage.Audio);
        }
        private void OnResolutionChanged(int index)
        {
            if (isRefreshingView)
            {
                return;
            }

            Resolution resolution = availableResolutions[index];
            currentData.ResolutionWidth = resolution.width;
            currentData.ResolutionHeight = resolution.height;
            currentData.RefreshRate = GetRefreshRateHz(resolution);
        }
        private void OnFullScreenChanged(bool isOn)
        {
            if (!isRefreshingView)
            {
                currentData.FullScreen = isOn;
            }
        }
        private void OnVSyncChanged(bool isOn)
        {
            if (!isRefreshingView)
            {
                currentData.VSync = isOn;
            }
        }
        private void OnFrameRateChanged(int index)
        {
            if (!isRefreshingView)
            {
                currentData.FrameRateLimit = frameRateOptions[index];
            }
        }
        private void OnMasterVolumeChanged(float value)
        {
            if (!isRefreshingView)
            {
                currentData.MasterVolume = value;
            }
        }
        private void OnMusicVolumeChanged(float value)
        {
            if (!isRefreshingView)
            {
                currentData.MusicVolume = value;
            }
        }
        private void OnSfxVolumeChanged(float value)
        {
            if (!isRefreshingView)
            {
                currentData.SfxVolume = value;
            }
        }

        private enum SettingsPage
        {
            Display,
            Audio
        }
        private static int GetRefreshRateHz(Resolution resolution)
        {
            // Unity 2022.3推荐使用refreshRateRatio, UI中只展示整数Hz方便玩家选择.
            return Mathf.RoundToInt((float)resolution.refreshRateRatio.value);
        }
    }
}
