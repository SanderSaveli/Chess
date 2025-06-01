using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class LoginScreen : UIScreen
    {
        [Header(H.Components)]
        [SerializeField] private ProfileManager _profileManager;

        [Space]
        [SerializeField] private ValidationInputField _nameField;
        [SerializeField] private ValidationInputField _passwordField;

        [Space]
        [SerializeField] private LoadButton _loadButton;
        [SerializeField] private TMP_Text _error;

        private IAccountManager _accountManager;

        private new void Start()
        {
            _error.gameObject.SetActive(false);
            base.Start();
        }

        [Inject]
        public void Construct(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        public void SendLogin()
        {
            if (_nameField.Validate() && _passwordField.Validate())
            {
                PrepareError();
                _loadButton.SetLoad(true);
                _accountManager.Login(_nameField.Field.text, _passwordField.Field.text, HandleSuccsess, HandleFail);
            }
        }

        private void HandleSuccsess()
        {
            _loadButton.SetLoad(false);
            if (IsShowen)
            {
                _profileManager.OpenProfile();
                Hide();
            }
        }

        private void PrepareError()
        {
            _error.gameObject.SetActive(false);
            Color color = _error.color;
            color.a = 1;
            _error.color = color;
        }

        private void HandleFail(string error)
        {
            _loadButton.SetLoad(false);
            _error.gameObject.SetActive(true);
            _error.text = error;

            _error.DOFade(0, 5f).SetEase(Ease.InSine).OnComplete(() =>
            {
                _error.gameObject.SetActive(false);
            });
        }
    }
}
