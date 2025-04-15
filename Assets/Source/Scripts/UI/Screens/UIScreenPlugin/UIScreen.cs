using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class UIScreen : MonoBehaviour
    {
        [Header(H.Components)]
        [SerializeField] private ScreenAnimator _animator;

        [Header(H.Params)]
        [SerializeField] private bool _isShowAtStart;

        private float _animationDurention;

        [Inject]
        public void Construct(IProjectSettings projectSettings)
        {
            _animationDurention = projectSettings.ScreenAnimationDuration;
        }

        private void Start()
        {
            _animator = GetComponent<ScreenAnimator>();
            if (_isShowAtStart)
            {
                gameObject.SetActive(true);
                _animator.AnimateShow(0, null);
            }
            else
            {
                _animator.AnimateHide(0, DisableGameobject);
            }
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
            _animator.AnimateShow(_animationDurention, null);
        }

        public virtual void Hide()
        {
            _animator.AnimateHide(_animationDurention, DisableGameobject);
        }

        private void DisableGameobject()
        {
            gameObject.SetActive(false);
        }
    }
}
