using Singletones;
using UnityEngine;

namespace OFG.ChessPeak
{
    public sealed class AudioController : DontDestroyOnLoadSingletone<AudioController>
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private AudioSource _soundSource;

        [Header("Sounds:")]
        [SerializeField] private SoundData _buttonSound;
        [SerializeField] private SoundData _figureSelectedSound;
        [SerializeField] private SoundData _figureUnselectedSound;
        [SerializeField] private SoundData _figureMoveSound;
        [SerializeField] private SoundData _winSound;
        [SerializeField] private SoundData _loseSound;
        [SerializeField] private SoundData _cardSelectedSound;
        [SerializeField] private SoundData _figurePlasedSound;

        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            EventBusProvider.EventBus.RegisterCallback<EventFigureSelected>(PlayFigureSelectedSound);
            EventBusProvider.EventBus.RegisterCallback<EventWinning>(PlayWinSound);
            EventBusProvider.EventBus.RegisterCallback<EventLosing>(PlayLoseSound);
            EventBusProvider.EventBus.RegisterCallback<EventFigureMoved>(PlayFigureMoveSound);
            EventBusProvider.EventBus.RegisterCallback<EventCardSelected>(PlayCardSelectedSound);
            EventBusProvider.EventBus.RegisterCallback<EventFigurePlacedInBuilder>(PlayFigurePlacedSound);
        }

        public void PlayButtonSound() => PlaySound(_buttonSound);

        public void PlayFigureSelectedSound(EventFigureSelected ctx) => PlaySound(_figureSelectedSound);

        public void PlayFigureUnselectedSound() => PlaySound(_figureUnselectedSound);

        public void PlayFigureMoveSound(EventFigureMoved ctx)
        {
            PlaySound(_figureMoveSound);
        }

        public void PlayWinSound(EventWinning ctx) => PlaySound(_winSound);

        public void PlayLoseSound(EventLosing ctx) => PlaySound(_loseSound);

        public void PlayCardSelectedSound(EventCardSelected ctx) => PlaySound(_cardSelectedSound);

        public void PlayFigurePlacedSound(EventFigurePlacedInBuilder ctx) => PlaySound(_figurePlasedSound);

        private void PlaySound(SoundData soundData) =>
            _soundSource.PlayOneShot(soundData.Clip, soundData.Volume);
    }
}
