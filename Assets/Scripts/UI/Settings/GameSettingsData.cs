using System;
using UnityEngine;

namespace Game.UI.Settings
{
    [Serializable]
    public struct GameSettingsData
    {
        public int ResolutionWidth;
        public int ResolutionHeight;
        public int RefreshRate;
        public bool FullScreen;
        public bool VSync;
        public int FrameRateLimit;
        public float MasterVolume;
        public float MusicVolume;
        public float SfxVolume;
        public static GameSettingsData CreateDefault()
        {
            // 默认配置保持接近当前设备状态, 首次启动时避免强行切换显示模式.
            return new GameSettingsData
            {
                ResolutionWidth = UnityEngine.Screen.currentResolution.width,
                ResolutionHeight = UnityEngine.Screen.currentResolution.height,
                RefreshRate = Mathf.RoundToInt((float)UnityEngine.Screen.currentResolution.refreshRateRatio.value),
                FullScreen = UnityEngine.Screen.fullScreen,
                VSync = UnityEngine.QualitySettings.vSyncCount > 0,
                FrameRateLimit = 60,
                MasterVolume = 1f,
                MusicVolume = 1f,
                SfxVolume = 1f
            };
        }
    }
}
