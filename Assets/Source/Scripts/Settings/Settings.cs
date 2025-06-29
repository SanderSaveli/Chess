using Enums;
using System;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class Settings : MonoBehaviour, IProjectSettings
    {
        public float SoundVolume { get => _soundVolume.Value; set => SetSound(value); }
        public float MusicVolume { get => _musicVolume.Value; set => SetMusic(value); }
        public float ScreenAnimationDuration { get => _screensAnimationDuration.Value; set => SetAnimation(value); }
        public string LanguageKey { get => _languageKey.Value; set => _languageKey.Value = value; }
        public Action<float> OnSoundVolumeChange { get; set; }
        public Action<float> OnMusicVolumeChange { get; set; }
        public Action<float> OnAnimationDurationChange { get ; set; }

        private FloatPrefsValue _soundVolume;
        private FloatPrefsValue _musicVolume;

        private FloatPrefsValue _screensAnimationDuration;
        private StringPrefsValue _languageKey;

        private void Awake()
        {
            _soundVolume = new FloatPrefsValue(Const.SOUND_VOLUME_KEY, 1);
            _musicVolume = new FloatPrefsValue(Const.MUSIC_VOLUME_KEY, 1);

            _screensAnimationDuration = new FloatPrefsValue(Const.SCREENS_ANIMATION_KEY, 1f);
            _languageKey = new StringPrefsValue(Const.LANGUAGE_KEY, TypeLocale.EN.ToString());
        }

        private void SetMusic(float value)
        {
            _musicVolume.Value = value;
            OnMusicVolumeChange?.Invoke(value);
        }

        private void SetSound(float value)
        {
            _soundVolume.Value = value;
            OnSoundVolumeChange?.Invoke(value);
        }

        private void SetAnimation(float value)
        {
            _screensAnimationDuration.Value = value;
            OnAnimationDurationChange?.Invoke(value);
            Debug.Log(_screensAnimationDuration.Value);
        }
    }
}
