using OFG.Chess;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace UDK.Audio
{
    public class AudioController : IAudioController, ISoundPlayer, IMusicPlayer, IDisposable
    {
        private readonly SavableValue<bool> _soundEnabled = new SavableValue<bool>("Audio.AudioController.soundEnabled", true);
        private readonly SavableValue<bool> _musicEnabled = new SavableValue<bool>("Audio.AudioController.musicEnabled", true);
        private readonly SavableValue<float> _soundVolume = new SavableValue<float>("Audio.AudioController.soundVolume", 1f);
        private readonly SavableValue<float> _musicVolume = new SavableValue<float>("Audio.AudioController.musicVolume", 1f);
        private readonly Dictionary<int, AudioSourceData> _sourceMedia = new Dictionary<int, AudioSourceData>();

        private AudioList _soundAudioList;
        private AudioList _musicAudioList;
        private AudioPresenter _presenter;
        private int _audioCodeIndex;

        bool IAudioController.SoundEnabled
        {
            get { return _soundEnabled.Value; }
            set
            {
                if (_soundEnabled.Value != value)
                {
                    foreach (var key in _sourceMedia.Keys)
                    {
                        var sourceData = _sourceMedia[key];
                        if (!sourceData.IsMusic)
                        {
                            sourceData.Source.volume = value ? sourceData.RequestedVolume * _soundVolume.Value : 0;
                        }
                    }

                    _soundEnabled.Value = value;
                }
            }
        }

        bool IAudioController.MusicEnabled
        {
            get { return _musicEnabled.Value; }
            set
            {
                if (_musicEnabled.Value != value)
                {
                    foreach (var key in _sourceMedia.Keys)
                    {
                        var sourceData = _sourceMedia[key];
                        if (sourceData.IsMusic)
                        {
                            sourceData.Source.volume = value ? sourceData.RequestedVolume * _musicVolume.Value : 0;
                        }
                    }

                    _musicEnabled.Value = value;
                }
            }
        }

        float IAudioController.SoundVolume
        {
            get { return _soundVolume.Value; }
            set
            {
                if (value > 1)
                    value = 1;
                else if (value < 0)
                    value = 0; 

                _soundVolume.Value = value;

                if (!_soundEnabled.Value)
                    return;

                foreach (var key in _sourceMedia.Keys)
                {
                    var sourceData = _sourceMedia[key];
                    if (!sourceData.IsMusic)
                    {
                        sourceData.Source.volume = sourceData.RequestedVolume * _soundVolume.Value;
                    }
                }
            }
        }

        float IAudioController.MusicVolume
        {
            get { return _musicVolume.Value; }
            set
            {
                if (value > 1)
                    value = 1;
                else if (value < 0)
                    value = 0;

                _musicVolume.Value = value;

                if (!_musicEnabled.Value)
                    return;

                foreach (var key in _sourceMedia.Keys)
                {
                    var sourceData = _sourceMedia[key];
                    if (sourceData.IsMusic)
                    {
                        sourceData.Source.volume = sourceData.RequestedVolume * _musicVolume.Value;
                    }
                }
            }
        }

        public AudioController(AudioList musicList = null, AudioList soundList = null)
        {
            Initialization(musicList, soundList);
        }

        void IDisposable.Dispose()
        {
            _sourceMedia.Clear();
            var g = _presenter.gameObject;
            _presenter = null;
#if UNITY_EDITOR
            GameObject.DestroyImmediate(g);
#else
            GameObject.Destroy(g);
#endif
        }

        int IMusicPlayer.PlayMusicClip(AudioClip clip, float volumeProportion)
        {
            if (volumeProportion > 1)
                volumeProportion = 1;
            else if (volumeProportion < 0)
                volumeProportion = 0;

            ScanForEndedSources();
            _audioCodeIndex++;

            var source = _presenter.gameObject.AddComponent<AudioSource>();
            source.clip = clip;
            source.loop = true;
            source.priority = 0;
            source.spatialBlend = 0;
            source.minDistance = 0.06f;

            var data = new AudioSourceData()
            {
                Is3Dsound = false,
                IsMusic = true,
                OnPause = false,
                RequestedVolume = volumeProportion,
                Source = source,
                SourceRequestedPos = Vector3.one,
                AudioCode = _audioCodeIndex,
            };

            source.volume = _musicEnabled.Value ? volumeProportion * _musicVolume.Value : 0;
            source.Play();
            _sourceMedia.Add(data.AudioCode, data);
            return _audioCodeIndex;
        }

        int IMusicPlayer.PlayMusicClip(string name, float volumeProportion)
        {
            AudioClip clip =  _musicAudioList.GetClipByName(name);
            if(clip == null)
                Debug.LogWarning("Audio clip with name " + name + " not found.");
            return ((IMusicPlayer)this).PlayMusicClip(clip, volumeProportion);
        }

        void IMusicPlayer.StopPlayingMusicClip(int audioCode)
        {
            if (!_sourceMedia.ContainsKey(audioCode))
                return;

            var s = _sourceMedia[audioCode];
            _sourceMedia.Remove(audioCode);
            s.Source.Stop();
            SmartDestroy(s.Source);
        }

        void IMusicPlayer.PausePlayingClip(int audioCode)
        {
            if (!_sourceMedia.ContainsKey(audioCode))
                return;

            var s = _sourceMedia[audioCode];
            s.Source.Pause();
            s.OnPause = true;
        }

        void IMusicPlayer.ResumeClipIfInPause(int audioCode)
        {
            if (!_sourceMedia.ContainsKey(audioCode))
                return;

            var s = _sourceMedia[audioCode];
            s.Source.UnPause();
            s.OnPause = false;
        }

        bool IMusicPlayer.IsMusicClipCodePlaying(int audioCode)
        {
            if (!_sourceMedia.ContainsKey(audioCode))
                return false;

            var s = _sourceMedia[audioCode];
            return s.Source.isPlaying;
        }

        int ISoundPlayer.PlaySound2D(AudioClip clip, float volumeProportion, bool looped)
        {
            if (volumeProportion > 1)
                volumeProportion = 1;
            else if (volumeProportion < 0)
                volumeProportion = 0; 

            ScanForEndedSources();
            _audioCodeIndex++;

            var source = _presenter.gameObject.AddComponent<AudioSource>();
            source.clip = clip;
            source.loop = looped;
            source.spatialBlend = 0;
            source.minDistance = 0.06f;
            var data = new AudioSourceData()
            {
                Is3Dsound = false,
                IsMusic = false,
                OnPause = false,
                RequestedVolume = volumeProportion,
                Source = source,
                SourceRequestedPos = Vector3.one,
                AudioCode = _audioCodeIndex,
            };

            source.volume = _soundEnabled.Value ? volumeProportion*_soundVolume.Value : 0;
            source.Play();
            _sourceMedia.Add(data.AudioCode, data);
            return _audioCodeIndex;
        }

        public int PlaySound2D(string name, float volumeProportion = 1, bool looped = false)
        {
            AudioClip clip = _soundAudioList.GetClipByName(name);
            if (clip == null)
                Debug.LogWarning("Audio clip with name " + name + " not found.");
            return ((ISoundPlayer)this).PlaySound2D(clip, volumeProportion, looped);
        }

        int ISoundPlayer.PlaySound3D(AudioClip clip, Vector3 position, float maxSoundDistance, float volumeProportion, bool looped)
        {
            if (volumeProportion > 1)
                volumeProportion = 1;
            else if (volumeProportion < 0)
                volumeProportion = 0; 

            ScanForEndedSources();
            _audioCodeIndex++;

            var go = new GameObject("Audio 3D source");
            go.transform.SetParent(_presenter.transform);
            go.transform.position = position;
            var source = go.AddComponent<AudioSource>();
            source.clip = clip;
            source.loop = looped;
            source.spatialBlend = 1;
            source.dopplerLevel = 0;
            source.reverbZoneMix = 0;
            source.pitch = 1 + Random.Range(-0.1f, 0.1f);
            source.minDistance = 0.4f;
            source.rolloffMode = AudioRolloffMode.Logarithmic;

            source.maxDistance = maxSoundDistance;

            var data = new AudioSourceData()
            {
                Is3Dsound = true,
                IsMusic = false,
                OnPause = false,
                RequestedVolume = volumeProportion,
                Source = source,
                SourceRequestedPos = position,
                AudioCode = _audioCodeIndex,
                CachedTransform = source.transform,
            };

            source.volume = _soundEnabled.Value ? volumeProportion * _soundVolume.Value : 0;
            source.Play();
            _sourceMedia.Add(data.AudioCode, data);
            return _audioCodeIndex;
        }

        public int PlaySound3D(string name, Vector3 position, float maxSoundDistance, float volumeProportion = 1, bool looped = false)
        {
            AudioClip clip = _soundAudioList.GetClipByName(name);
            if (clip == null)
                Debug.LogWarning("Audio clip with name " + name + " not found.");
            return ((ISoundPlayer)this).PlaySound3D(clip, position, maxSoundDistance, volumeProportion, looped);
        }

        void ISoundPlayer.StopPlayingClip(int audioCode)
        {
            if (!_sourceMedia.ContainsKey(audioCode)) 
                return;

            var s = _sourceMedia[audioCode];
            _sourceMedia.Remove(audioCode);
            s.Source.Stop();

            if (s.Is3Dsound)
            {
                SmartDestroy(s.Source.gameObject);
            }
            else
            {
                SmartDestroy(s.Source);
            }
        }

        bool ISoundPlayer.IsAudioClipCodePlaying(int audioCode)
        {
            if (!_sourceMedia.ContainsKey(audioCode))
                return false;

            var s = _sourceMedia[audioCode];
            return s.Source.isPlaying;
        }

        void ISoundPlayer.SetAudioListenerToPosition(Vector3 position)
        {
            _presenter.AudioListener.transform.position = position;
        }

        void ISoundPlayer.SetSourcePositionTo(int audioCode, Vector3 destinationPos)
        {
            if (!_sourceMedia.ContainsKey(audioCode))
                return;

            var data = _sourceMedia[audioCode];
            if (!data.Is3Dsound)
            {
                Debug.LogError("try control 2d sound as 3d sound");
                return;
            }

            if (data.SourceRequestedPos == destinationPos)
                return;

            data.SourceRequestedPos = destinationPos;
            data.CachedTransform.position = destinationPos;
        }

        private void Initialization(AudioList musicList, AudioList soundList)
        {
            _musicAudioList = musicList;    
            _soundAudioList = soundList;
            _presenter = new GameObject("AUDIO").AddComponent<AudioPresenter>();
            _presenter.gameObject.AddComponent<DontDestroy>();
            var listener = new GameObject("LISTENER").AddComponent<AudioListener>();
            listener.gameObject.AddComponent<DontDestroy>();
            listener.transform.SetParent(_presenter.transform);
            _presenter.AudioListener = listener;
        }

        private void ScanForEndedSources()
        {
            var todel = new Dictionary<int, AudioSourceData>();

            foreach (var k in _sourceMedia.Keys)
            {
                var source = _sourceMedia[k];
                if (!source.OnPause && !source.Source.isPlaying)
                    todel.Add(k, source);
            }

            foreach (var k in todel.Keys)
            {
                var source = todel[k];
                _sourceMedia.Remove(k);

                if (source.Is3Dsound && !source.IsMusic)
                    SmartDestroy(source.Source.gameObject);
                else
                    SmartDestroy(source.Source);
            }
        }
        
        private void SmartDestroy(Object obj)
        {
            #if UNITY_EDITOR
            Object.DestroyImmediate(obj);
            #else
            Object.Destroy(obj);
            #endif
        }

        internal class AudioSourceData
        {
            public AudioSource Source;
            public int AudioCode;
            public bool IsMusic;
            public bool Is3Dsound;
            public Vector3 SourceRequestedPos;
            public float RequestedVolume;
            public bool OnPause;
            public Transform CachedTransform;
        }
    }
}