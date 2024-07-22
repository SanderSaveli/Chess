using System;
using UnityEngine;

namespace IUP.Toolkit
{
    [Serializable]
    public sealed class TweenFloat : TweenBase<float>
    {
        public TweenFloat() : base(Mathf.Lerp) { }
    }
}
