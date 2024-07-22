using System;
using UnityEngine;

namespace IUP.Toolkit
{
    [Serializable]
    public sealed class TweenVector4 : TweenBase<Vector4>
    {
        public TweenVector4() : base(Vector4.Lerp) { }
    }
}
