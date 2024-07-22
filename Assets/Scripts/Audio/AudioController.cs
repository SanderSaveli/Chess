using UnityEngine;

namespace OFG.ChessPeak
{
    public sealed class AudioController : MonoBehaviour
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

        public void PlayButtonSound() => PlaySound(_buttonSound);

        public void PlayFigureSelectedSound() => PlaySound(_figureSelectedSound);

        public void PlayFigureUnselectedSound() => PlaySound(_figureUnselectedSound);

        public void PlayFigureMoveSound() => PlaySound(_figureMoveSound);

        public void PlayWinSound() => PlaySound(_winSound);

        public void PlayLoseSound() => PlaySound(_loseSound);

        private void PlaySound(SoundData soundData) =>
            _soundSource.PlayOneShot(soundData.Clip, soundData.Volume);
    }
}
