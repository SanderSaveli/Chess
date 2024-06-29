using System;
using System.Collections.Generic;
using UnityEngine;

namespace UDK.Audio
{
    [Serializable]
    public class AudioData
    {
        public string Name;
        public AudioClip clip;
    }
    [CreateAssetMenu(fileName = "new AudioList", menuName = "Audio/AudioList")]
    public class AudioList : ScriptableObject
    {
        public List<AudioData> data = new();
        public AudioClip GetClipByName(string name)
        {
            foreach (var audioData in data)
            {
                if (audioData.Name == name)
                {
                    return audioData.clip;
                }
            }
            return null; 
        }
    }

}
