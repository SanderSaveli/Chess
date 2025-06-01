using System;
using UnityEngine;

namespace OFG.ChessPeak
{
    public abstract class ScreenAnimator : MonoBehaviour
    {
        public abstract void AnimateShow(float duration, Action callback);

        public abstract void AnimateHide(float duration, Action callback);
    }
}
