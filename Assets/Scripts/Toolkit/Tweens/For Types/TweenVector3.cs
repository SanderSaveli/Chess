using System;
using UnityEngine;

namespace IUP.Toolkit
{
    [Serializable]
    public sealed class TweenVector3 : TweenBase<Vector3>
    {
        public TweenVector3() : base(Vector3.Lerp) { }
    }
}
