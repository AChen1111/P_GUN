using UnityEngine;
using UnityEngine.Audio;

namespace Game.UI.Settings
{
    public static class GameSettingsStore
    {
        private const string ResolutionWidthKey = "Settings.ResolutionWidth";
        private const string ResolutionHeightKey = "Settings.ResolutionHeight";
        private const string RefreshRateKey = "Settings.RefreshRate";
        private const string FullScreenKey = "Settings.FullScreen";
        private const string VSyncKey = "Settings.VSync";
        private const string FrameRateLimitKey = "Settings.FrameRateLimit";
        private const string MasterVolumeKey = "Settings.MasterVolume";
        private const string MusicVolumeKey = "Settings.MusicVolume";
        private const string SfxVolumeKey = "Settings.SfxVolume";

        public static GameSettingsData Load()
        {
            GameSettingsData defaultData = GameSettingsData.CreateDefault();

            // PlayerPrefs只保存玩家本地偏好, 不承担玩法数据或进度数据.
            return new GameSettingsData
            {
                ResolutionWidth = PlayerPrefs.GetInt(ResolutionWidthKey, defaultData.ResolutionWidth),
                ResolutionHeight = PlayerPrefs.GetInt(ResolutionHeightKey, defaultData.ResolutionHeight),
                RefreshRate = PlayerPrefs.GetInt(RefreshRateKey, defaultData.RefreshRate),
                FullScreen = PlayerPrefs.GetInt(FullScreenKey, defaultData.FullScreen ? 1 : 0) == 1,
                VSync = PlayerPrefs.GetInt(VSyncKey, defaultData.VSync ? 1 : 0) == 1,
                FrameRateLimit = PlayerPrefs.GetInt(FrameRateLimitKey, defaultData.FrameRateLimit),
                MasterVolume = PlayerPrefs.GetFloat(MasterVolumeKey, defaultData.MasterVolume),
                MusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, defaultData.MusicVolume),
                SfxVolume = PlayerPrefs.GetFloat(SfxVolumeKey, defaultData.SfxVolume)
            };
        }

        public static void Save(GameSettingsData data)
        {
            PlayerPrefs.SetInt(ResolutionWidthKey, data.ResolutionWidth);
            PlayerPrefs.SetInt(ResolutionHeightKey, data.ResolutionHeight);
            PlayerPrefs.SetInt(RefreshRateKey, data.RefreshRate);
            PlayerPrefs.SetInt(FullScreenKey, data.FullScreen ? 1 : 0);
            PlayerPrefs.SetInt(VSyncKey, data.VSync ? 1 : 0);
            PlayerPrefs.SetInt(FrameRateLimitKey, data.FrameRateLimit);
            PlayerPrefs.SetFloat(MasterVolumeKey, data.MasterVolume);
            PlayerPrefs.SetFloat(MusicVolumeKey, data.MusicVolume);
            PlayerPrefs.SetFloat(SfxVolumeKey, data.SfxVolume);
            PlayerPrefs.Save();
        }

        public static void Apply(GameSettingsData data, AudioMixer audioMixer, string masterVolumeParameter, string musicVolumeParameter, string sfxVolumeParameter)
        {
            QualitySettings.vSyncCount = data.VSync ? 1 : 0;
            Application.targetFrameRate = data.VSync ? -1 : data.FrameRateLimit;

            // 新版Unity使用RefreshRate结构体, 这里把设置面板保存的整数Hz转成精确刷新率参数.
            var refreshRate = new RefreshRate
            {
                numerator = (uint)Mathf.Max(1, data.RefreshRate),
                denominator = 1
            };
            FullScreenMode fullScreenMode = data.FullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
            Screen.SetResolution(data.ResolutionWidth, data.ResolutionHeight, fullScreenMode, refreshRate);

            if (audioMixer == null)
            {
                Debug.LogError("设置应用失败, AudioMixer不能为空.");
                return;
            }

            SetMixerVolume(audioMixer, masterVolumeParameter, data.MasterVolume);
            SetMixerVolume(audioMixer, musicVolumeParameter, data.MusicVolume);
            SetMixerVolume(audioMixer, sfxVolumeParameter, data.SfxVolume);
        }

        private static void SetMixerVolume(AudioMixer audioMixer, string parameterName, float normalizedVolume)
        {
            if (string.IsNullOrEmpty(parameterName))
            {
                Debug.LogError("设置应用失败, AudioMixer参数名不能为空.");
                return;
            }

            // 线性音量转分贝, 0时使用-80dB作为静音近似值.
            float mixerValue = normalizedVolume <= 0.0001f ? -80f : Mathf.Log10(normalizedVolume) * 20f;

            if (!audioMixer.SetFloat(parameterName, mixerValue))
            {
                Debug.LogError($"设置应用失败, AudioMixer未暴露参数: {parameterName}.");
            }
        }
    }
}
