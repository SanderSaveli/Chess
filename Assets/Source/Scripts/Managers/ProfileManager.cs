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
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus, IAccountManager accountManager)
        {
            _signalBus = signalBus;
            _accountManager = accountManager;
        }

        public void OpenProfile()
        {
            if(_accountManager.IsInAccount)
            {
                _signalBus.Fire(new SignalInputOpenWindow(MenuScreens.profile));
            }
            else
            {
                _signalBus.Fire(new SignalInputOpenWindow(MenuScreens.register));
            }
        }
    }
}
