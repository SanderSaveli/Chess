using UnityEngine;

namespace OFG.ChessPeak
{
    public class Settings : MonoBehaviour, IProjectSettings
    {
        public float SoundVolume { get => _soundVolume.Value; set => _soundVolume.Value = value; }
        public float MusicVolume { get => _musicVolume.Value; set => _musicVolume.Value = value; }
        public float ScreenAnimationDuration { get => _screensAnimationDuration.Value; set => _screensAnimationDuration.Value = value; }

        private FloatPrefsValue _soundVolume;
        private FloatPrefsValue _musicVolume;

        private FloatPrefsValue _screensAnimationDuration;

        private void Awake()
        {
            _soundVolume = new FloatPrefsValue(Const.SOUND_VOLUME_KEY, 1);
            _musicVolume = new FloatPrefsValue(Const.MUSIC_VOLUME_KEY, 1);

            _screensAnimationDuration = new FloatPrefsValue(Const.SCREENS_ANIMATION_KEY, 0.5f);
        }
    }
}
