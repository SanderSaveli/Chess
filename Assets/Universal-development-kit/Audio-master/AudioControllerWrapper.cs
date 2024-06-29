using UnityEngine;
using Singletones;
namespace UDK.Audio
{
    public class AudioControllerWrapper : DontDestroyOnLoadSingletone<AudioControllerWrapper>
    {
        [SerializeField] private AudioList soundList;
        [SerializeField] private AudioList musicList;
        private AudioController _audioController;

        private void OnEnable()
        {
            if(_audioController == null)
            {
                InstantController();
            }
        }
        public ISoundPlayer soundPlayer
        {
            get
            {
                if (_audioController == null)
                    InstantController();
                return _audioController;
            }
        }
        public IMusicPlayer musicPlayer
        {
            get
            {
                if (_audioController == null)
                    InstantController();
                return _audioController;
            }
        }
        public IAudioController audioController
        {
            get
            {
                if (_audioController == null)
                    InstantController();
                return _audioController;
            }
        }

        private void InstantController()
        {
            _audioController = new AudioController(musicList, soundList);
        }

    }

}
