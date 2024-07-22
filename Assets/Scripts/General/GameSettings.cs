using IUP.Toolkit;

namespace OFG.ChessPeak
{
    public static class GameSettings
    {
        public const float DefaultMusicVolume = 0.5f;
        public const float DefaultSoundVolume = 0.5f;
        public const bool DefaultIsMusicEnabled = true;
        public const bool DefaultIsSoundEnabled = true;

        public static float MusicVolume
        {
            get => AdvancedPrefs.GetFloatOrDefault(PrefsKey.MusicVolume, DefaultMusicVolume);
            set => AdvancedPrefs.SetSaveFloat(PrefsKey.MusicVolume, value);
        }
        public static float SoundVolume
        {
            get => AdvancedPrefs.GetFloatOrDefault(PrefsKey.SoundVolume, DefaultSoundVolume);
            set => AdvancedPrefs.SetSaveFloat(PrefsKey.SoundVolume, value);
        }
        public static bool IsMusicEnabled
        {
            get => AdvancedPrefs.GetBoolOrDefault(PrefsKey.IsMusicEnabled, DefaultIsMusicEnabled);
            set => AdvancedPrefs.SetBool(PrefsKey.IsMusicEnabled, value);
        }
        public static bool IsSoundEnabled
        {
            get => AdvancedPrefs.GetBoolOrDefault(PrefsKey.IsMusicEnabled, DefaultIsSoundEnabled);
            set => AdvancedPrefs.SetBool(PrefsKey.IsMusicEnabled, value);
        }
    }
}
