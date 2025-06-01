using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class RegistationScreen : UIScreen
    {
        [Header(H.Components)]
        [SerializeField] private ProfileManager _profileManager;

        [Space]
        [SerializeField] private ValidationInputField _nameField;
        [SerializeField] private ValidationInputField _passwordField;

        [Space]
        [SerializeField] private LoadButton _loadButton;
        [SerializeField] private MessageUiScreen _error;

        private IAccountManager _accountManager;

        public new void Start()
        {
            _error.gameObject.SetActive(false);
            base.Start();
        }

        [Inject]
        public void Construct(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        public void SendRegister()
        {
            if (_nameField.Validate() && _passwordField.Validate())
            {
                PrepareError();
                _loadButton.SetLoad(true);
                _accountManager.Register(_nameField.Field.text, _passwordField.Field.text, HandleSuccsess, HandleFail);
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
            _error.Hide();
        }

        private void HandleFail(string error)
        {
            _loadButton.SetLoad(false);
            _error.gameObject.SetActive(true);
            _error.Show();
            _error.UpdateMessage("default_registration_error");
        }
    }
}
