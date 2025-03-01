using System;
using UnityEngine;

namespace OFG.ChessPeak
{
    [Serializable]
    public struct SoundData
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField][Range(0.0f, 1.0f)] private float _volumeScale;

        public readonly AudioClip Clip => _clip;
        public readonly float Volume => _volumeScale;
    }
}
