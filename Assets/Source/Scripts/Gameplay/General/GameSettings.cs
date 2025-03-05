using IUP.Toolkit;
using System;
using System.Diagnostics;
using UnityEngine;

namespace OFG.ChessPeak
{
    public static class GameSettings
    {
        public const float DefaultMusicVolume = 0.5f;
        public const float DefaultSoundVolume = 0.5f;
        public const bool DefaultIsMusicEnabled = true;
        public const bool DefaultIsSoundEnabled = true;

        public static Action<float> OnMusicVolumeChanged;
        public static Action<float> OnSoundVolumeChanged;

        public static float MusicVolume
        {
            get => AdvancedPrefs.GetFloatOrDefault(PrefsKey.MusicVolume, DefaultMusicVolume);
            set {
                AdvancedPrefs.SetSaveFloat(PrefsKey.MusicVolume, value);
                OnMusicVolumeChanged?.Invoke(value);
            }
        }
        public static float SoundVolume
        {
            get => AdvancedPrefs.GetFloatOrDefault(PrefsKey.SoundVolume, DefaultSoundVolume);
            set {
                AdvancedPrefs.SetSaveFloat(PrefsKey.SoundVolume, value);
                OnSoundVolumeChanged?.Invoke(value);
            }
        }
        public static bool IsMusicEnabled
        {
            get => AdvancedPrefs.GetBoolOrDefault(PrefsKey.IsMusicEnabled, DefaultIsMusicEnabled);
            set {
                if (value)
                {
                    OnMusicVolumeChanged?.Invoke(MusicVolume);
                }
                else
                {
                    OnMusicVolumeChanged?.Invoke(0);
                }
                AdvancedPrefs.SetBool(PrefsKey.IsMusicEnabled, value);
            }
        }
        public static bool IsSoundEnabled
        {
            get => AdvancedPrefs.GetBoolOrDefault(PrefsKey.IsMusicEnabled, DefaultIsSoundEnabled);
            set
            {
                if (value)
                {
                    OnSoundVolumeChanged?.Invoke(SoundVolume);
                }
                else
                {
                    OnSoundVolumeChanged?.Invoke(0);
                }
                AdvancedPrefs.SetBool(PrefsKey.IsMusicEnabled, value);
            }
        }
    }
}
