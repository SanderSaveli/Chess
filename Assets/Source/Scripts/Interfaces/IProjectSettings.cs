using System;

namespace OFG.ChessPeak
{
    public interface IProjectSettings
    {
        public float SoundVolume { get; set; }
        public float MusicVolume { get; set; }
        public float ScreenAnimationDuration { get; set; }
        public string LanguageKey { get; set; }

        public Action<float> OnSoundVolumeChange { get; set; }
        public Action<float> OnMusicVolumeChange { get; set; }
        public Action<float> OnAnimationDurationChange { get; set; }
    }
}
