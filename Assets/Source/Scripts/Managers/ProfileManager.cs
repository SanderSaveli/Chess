using TMPro;
using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class ProfileManager : MonoBehaviour
    {
        [Header(H.Components)]
        [SerializeField] private TMP_InputField _loginNameFiled;
        [SerializeField] private TMP_InputField _loginPasswordFiled;
        
        [Space]
        [SerializeField] private TMP_InputField _registerNameFiled;
        [SerializeField] private TMP_InputField _registerPasswordFiled;

        [Space]
        [SerializeField] private UIScreen _loginScreen;
        [SerializeField] private UIScreen _registerScreen;
        [SerializeField] private UIScreen _profileScreen;

        private IAccountManager _accountManager;

        [Inject]
        public void Construct(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        public void OpenProfile()
        {
            if(_accountManager.IsInAccount)
            {
                _profileScreen.Show();
            }
            else
            {
                _registerScreen.Show();
            }
        }

        public void Register()
        {
            _accountManager.Login(_registerNameFiled.text, _registerPasswordFiled.text, SuccsessLogin, ErrorRegister);
        }

        public void Login()
        {
            _accountManager.Login(_loginNameFiled.text, _loginPasswordFiled.text, SuccsessLogin, ErrorRegister);
        }

        private void SuccsessLogin()
        {

        }

        private void ErrorLogin()
        {

        }

        private void ErrorRegister()
        {

        }
    }
}
