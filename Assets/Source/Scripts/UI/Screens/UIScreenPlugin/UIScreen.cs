using System;
using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class UIScreen : MonoBehaviour
    {
        [Header(H.Components)]
        [SerializeField] private ScreenAnimator _animator;
        [SerializeField] private UIScreen _bg;

        [Header(H.Params)]
        [SerializeField] private bool _isShowAtStart;

        public bool IsShowen {  get; private set; }
        public bool IsShowAtStart => _isShowAtStart;

        public Action OnShowScreen;
        public Action OnHideScreen;
        private IProjectSettings _projectSettings;

        [Inject]
        public void Construct(IProjectSettings projectSettings)
        {
            _projectSettings = projectSettings;
        }
        private void Awake()
        {
            transform.localScale = Vector3.one;
        }
        public void Start()
        {
            _animator = GetComponent<ScreenAnimator>();
            IsShowen = _isShowAtStart;
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
            if(_animator == null)
            {
                _animator= gameObject.GetComponent<ScreenAnimator>();
            }

            IsShowen = true;
            gameObject.SetActive(true);
            Debug.Log(gameObject.name);
            Debug.Log(_animator);
            Debug.Log(_projectSettings);
            _animator.AnimateShow(_projectSettings.ScreenAnimationDuration, null);
            OnShowScreen?.Invoke();
            if(_bg != null)
                _bg.Show();
        }

        public virtual void Hide()
        {
            if (_animator == null)
            {
                _animator = gameObject.GetComponent<ScreenAnimator>();
            }

            IsShowen = false;
            _animator.AnimateHide(_projectSettings.ScreenAnimationDuration, DisableGameobject);
            OnHideScreen?.Invoke();
            if (_bg != null)
                _bg.Hide();
        }

        private void DisableGameobject()
        {
            gameObject.SetActive(false);
        }
    }
}
