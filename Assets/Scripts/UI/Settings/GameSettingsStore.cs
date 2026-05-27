using UnityEngine;
using UnityEngine.Audio;
using Game.Core;

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
        public static GameSettingsData Load()
        {
            GameSettingsData defaultData = GameSettingsData.CreateDefault();
            GameAudioSettings audioSettings = GameAudioSettingsStore.Load(defaultData.MasterVolume, defaultData.MusicVolume, defaultData.SfxVolume);

            // PlayerPrefs只保存玩家本地偏好, 不承担玩法数据或进度数据.
            return new GameSettingsData
            {
                ResolutionWidth = PlayerPrefs.GetInt(ResolutionWidthKey, defaultData.ResolutionWidth),
                ResolutionHeight = PlayerPrefs.GetInt(ResolutionHeightKey, defaultData.ResolutionHeight),
                RefreshRate = PlayerPrefs.GetInt(RefreshRateKey, defaultData.RefreshRate),
                FullScreen = PlayerPrefs.GetInt(FullScreenKey, defaultData.FullScreen ? 1 : 0) == 1,
                VSync = PlayerPrefs.GetInt(VSyncKey, defaultData.VSync ? 1 : 0) == 1,
                FrameRateLimit = PlayerPrefs.GetInt(FrameRateLimitKey, defaultData.FrameRateLimit),
                MasterVolume = audioSettings.MasterVolume,
                MusicVolume = audioSettings.MusicVolume,
                SfxVolume = audioSettings.SfxVolume
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
            GameAudioSettingsStore.Save(new GameAudioSettings(data.MasterVolume, data.MusicVolume, data.SfxVolume), false);
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

            GameAudioSettingsStore.Apply(new GameAudioSettings(data.MasterVolume, data.MusicVolume, data.SfxVolume), audioMixer, masterVolumeParameter, musicVolumeParameter, sfxVolumeParameter);
        }
    }
}
