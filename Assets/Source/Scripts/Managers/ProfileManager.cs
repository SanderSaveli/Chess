using TMPro;
using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class ProfileManager : MonoBehaviour
    {
        [Header(H.Components)]
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
    }
}
