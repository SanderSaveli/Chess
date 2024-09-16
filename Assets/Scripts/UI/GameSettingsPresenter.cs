using OFG.ChessPeak.UI;
using UnityEngine;

namespace OFG.ChessPeak
{
    public sealed class GameSettingsPresenter : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private IconSlider _musicSlider;
        [SerializeField] private IconSlider _soundSlider;

        private void Awake() => SubscribeOnEvents();

        private void OnDestroy() => UnsubscribeFromEvents();

        private void OnEnable() => SynchViewWithModel();

        private void SynchViewWithModel()
        {
            _musicSlider.SetStateWithoutNofications(GameSettings.IsMusicEnabled, GameSettings.MusicVolume);
            _soundSlider.SetStateWithoutNofications(GameSettings.IsSoundEnabled, GameSettings.SoundVolume);
        }

        private void OnMusicViewChanged(bool isInteractable, float value)
        {
            GameSettings.IsMusicEnabled = isInteractable;
            if(isInteractable)
            {
                GameSettings.MusicVolume = value;
            }
        }

        private void OnSoundViewChanged(bool isInteractable, float value)
        {
            GameSettings.IsSoundEnabled = isInteractable;
            if (isInteractable)
            {
                GameSettings.SoundVolume = value;
            }
        }

        private void SubscribeOnEvents()
        {
            _musicSlider.ValueChanged += OnMusicViewChanged;
            _soundSlider.ValueChanged += OnSoundViewChanged;
        }

        private void UnsubscribeFromEvents()
        {
            _musicSlider.ValueChanged -= OnMusicViewChanged;
            _soundSlider.ValueChanged -= OnSoundViewChanged;
        }
    }
}
