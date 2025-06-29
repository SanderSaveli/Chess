using Singletones;
using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public sealed class AudioController : DontDestroyOnLoadSingletone<AudioController>
    {
        [Header(H.Components)]
        [SerializeField] private AudioSource _soundSource;
        [SerializeField] private AudioSource _musicSource;

        [Header("Sounds:")]
        [SerializeField] private SoundData _buttonSound;
        [SerializeField] private SoundData _figureSelectedSound;
        [SerializeField] private SoundData _figureUnselectedSound;
        [SerializeField] private SoundData _figureMoveSound;
        [SerializeField] private SoundData _winSound;
        [SerializeField] private SoundData _loseSound;
        [SerializeField] private SoundData _cardSelectedSound;
        [SerializeField] private SoundData _figurePlasedSound;

        private float _soundVolume;
        private float _musicVolume;
        private IProjectSettings _projectSettings;

        [Inject]
        public void Construct(IProjectSettings projectSettings)
        {
            _projectSettings = projectSettings;
        }

        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void Start()
        {
            SetMusicVolume(_projectSettings.MusicVolume);
            SetSoundVolume(_projectSettings.SoundVolume);
        }

        private void OnDisable()
        {
            UnsbscribeToEvents();
        }

        private void SetMusicVolume(float volume)
        {
            _musicVolume = volume;
            _musicSource.volume = volume;
        }
        private void SetSoundVolume(float volume)
        {
            _soundVolume = volume;
            _soundSource.volume = volume;
        }

        private void SubscribeToEvents()
        {
            EventBusProvider.EventBus.RegisterCallback<EventFigureSelected>(PlayFigureSelectedSound);
            EventBusProvider.EventBus.RegisterCallback<EventWinning>(PlayWinSound);
            EventBusProvider.EventBus.RegisterCallback<EventLosing>(PlayLoseSound);
            EventBusProvider.EventBus.RegisterCallback<EventFigureMoved>(PlayFigureMoveSound);
            EventBusProvider.EventBus.RegisterCallback<EventCardSelected>(PlayCardSelectedSound);
            EventBusProvider.EventBus.RegisterCallback<EventFigurePlacedInBuilder>(PlayFigurePlacedSound);

            _projectSettings.OnMusicVolumeChange += SetMusicVolume;
            _projectSettings.OnSoundVolumeChange += SetSoundVolume;
        }

        private void UnsbscribeToEvents()
        {
            EventBusProvider.EventBus.UnregisterCallback<EventFigureSelected>(PlayFigureSelectedSound);
            EventBusProvider.EventBus.UnregisterCallback<EventWinning>(PlayWinSound);
            EventBusProvider.EventBus.UnregisterCallback<EventLosing>(PlayLoseSound);
            EventBusProvider.EventBus.UnregisterCallback<EventFigureMoved>(PlayFigureMoveSound);
            EventBusProvider.EventBus.UnregisterCallback<EventCardSelected>(PlayCardSelectedSound);
            EventBusProvider.EventBus.UnregisterCallback<EventFigurePlacedInBuilder>(PlayFigurePlacedSound);

            _projectSettings.OnMusicVolumeChange -= SetMusicVolume;
            _projectSettings.OnSoundVolumeChange -= SetSoundVolume;
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
            _soundSource.PlayOneShot(soundData.Clip, _soundVolume);
    }
}
