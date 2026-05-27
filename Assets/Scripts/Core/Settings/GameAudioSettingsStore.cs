using UnityEngine;
using UnityEngine.Audio;

namespace Game.Core
{
    public readonly struct GameAudioSettings
    {
        public readonly float MasterVolume;
        public readonly float MusicVolume;
        public readonly float SfxVolume;

        public GameAudioSettings(float masterVolume, float musicVolume, float sfxVolume)
        {
            MasterVolume = masterVolume;
            MusicVolume = musicVolume;
            SfxVolume = sfxVolume;
        }
    }

    public static class GameAudioSettingsStore
    {
        public const string AudioMixerAddress = "shared/audio/audiomixer/audiomixer";
        public const string MasterVolumeParameter = "Master";
        public const string MusicVolumeParameter = "BGM";
        public const string SfxVolumeParameter = "SFX";

        private const string MasterVolumeKey = "Settings.MasterVolume";
        private const string MusicVolumeKey = "Settings.MusicVolume";
        private const string SfxVolumeKey = "Settings.SfxVolume";

        public static GameAudioSettings Load(float defaultMasterVolume = 1f, float defaultMusicVolume = 1f, float defaultSfxVolume = 1f)
        {
            // 音频设置使用独立读取入口, 启动流程不需要依赖设置面板.
            return new GameAudioSettings(
                PlayerPrefs.GetFloat(MasterVolumeKey, defaultMasterVolume),
                PlayerPrefs.GetFloat(MusicVolumeKey, defaultMusicVolume),
                PlayerPrefs.GetFloat(SfxVolumeKey, defaultSfxVolume));
        }

        public static void Save(GameAudioSettings settings, bool saveImmediately = true)
        {
            PlayerPrefs.SetFloat(MasterVolumeKey, settings.MasterVolume);
            PlayerPrefs.SetFloat(MusicVolumeKey, settings.MusicVolume);
            PlayerPrefs.SetFloat(SfxVolumeKey, settings.SfxVolume);

            if (saveImmediately)
            {
                PlayerPrefs.Save();
            }
        }

        public static void Apply(AudioMixer audioMixer)
        {
            Apply(Load(), audioMixer, MasterVolumeParameter, MusicVolumeParameter, SfxVolumeParameter);
        }

        public static void Apply(GameAudioSettings settings, AudioMixer audioMixer, string masterVolumeParameter, string musicVolumeParameter, string sfxVolumeParameter)
        {
            if (audioMixer == null)
            {
                Debug.LogError("音频设置应用失败, AudioMixer不能为空.");
                return;
            }

            SetMixerVolume(audioMixer, masterVolumeParameter, settings.MasterVolume);
            SetMixerVolume(audioMixer, musicVolumeParameter, settings.MusicVolume);
            SetMixerVolume(audioMixer, sfxVolumeParameter, settings.SfxVolume);
        }

        private static void SetMixerVolume(AudioMixer audioMixer, string parameterName, float normalizedVolume)
        {
            if (string.IsNullOrEmpty(parameterName))
            {
                Debug.LogError("音频设置应用失败, AudioMixer参数名不能为空.");
                return;
            }

            // 线性音量转分贝, 0时使用-80dB作为静音近似值.
            float mixerValue = normalizedVolume <= 0.0001f ? -80f : Mathf.Log10(normalizedVolume) * 20f;

            if (!audioMixer.SetFloat(parameterName, mixerValue))
            {
                Debug.LogError($"音频设置应用失败, AudioMixer未暴露参数: {parameterName}.");
            }
        }
    }
}
