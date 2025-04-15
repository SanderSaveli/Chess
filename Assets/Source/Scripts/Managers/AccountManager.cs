using System;
using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class AccountManager : MonoBehaviour, IAccountManager
    {
        public string Name => _playerNetworkData.name;

        public bool IsInAccount => _isInAccount;

        private bool _isInAccount;
        private PlayerNetworkData _playerNetworkData;

        private INetworkManager _networkManager;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(INetworkManager networkManager, SignalBus signalBus)
        {
            _networkManager = networkManager;
            _signalBus = signalBus;
        }

        private void Start()
        {
            _playerNetworkData = new();
            if (PlayerPrefs.HasKey(Const.PLAYER_ID_KEY))
            {
                string id = PlayerPrefs.GetString(Const.PLAYER_ID_KEY);
                _networkManager.GetPlayerData(id, SetAccount, null);
            }
        }

        public void Login(string username, string password, Action succsess, Action error)
        {
            LoginNetworkData loginNetworkData = new LoginNetworkData(username, password);
            _networkManager.PostLoginPlayer(loginNetworkData, SetAccount, null);
        }

        public void Logout()
        {
            _isInAccount = false;
            _playerNetworkData = null;
            PlayerPrefs.DeleteKey(Const.PLAYER_ID_KEY);
        }

        private void SetAccount(PlayerNetworkData playerNetworkData)
        {
            _isInAccount = true;
            _playerNetworkData = playerNetworkData;
            _signalBus.Fire(new SignalPlayerAccountUpdated(playerNetworkData));
        }

        public void Register(string username, string password, Action succsess, Action error)
        {
            LoginNetworkData loginNetworkData = new LoginNetworkData(username, password);
            _networkManager.PostCreateNewPlayer(loginNetworkData, SetAccount, null);
        }
    }
}
