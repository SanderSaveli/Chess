using System;
using UnityEngine;

namespace IUP.Toolkit
{
    [Serializable]
    public sealed class TweenVector2 : TweenBase<Vector2>
    {
        public TweenVector2() : base(Vector2.Lerp) { }
    }
}
