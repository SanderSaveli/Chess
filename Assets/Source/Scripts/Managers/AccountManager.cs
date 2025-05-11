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
            _playerNetworkData = new();
            _networkManager = networkManager;
            _signalBus = signalBus;
        }

        private void Start()
        {
            _playerNetworkData = new();
            if (PlayerPrefs.HasKey(Const.PLAYER_ID_KEY))
            {
                int id = PlayerPrefs.GetInt(Const.PLAYER_ID_KEY);
                _networkManager.GetPlayerData(id.ToString(), SetAccount, null);
            }
        }

        public void Register(string username, string password, Action succsess, Action<string> error)
        {
            Debug.Log("GetRegistered");
            LoginNetworkData loginNetworkData = new LoginNetworkData(username, password);
            _networkManager.PostCreateNewPlayer(loginNetworkData,(a) => { SetAccount(a); succsess?.Invoke();}, error);
        }

        public void Login(string username, string password, Action succsess, Action<string> error)
        {
            LoginNetworkData loginNetworkData = new LoginNetworkData(username, password);
            _networkManager.PostLoginPlayer(loginNetworkData, (a) => { SetAccount(a); succsess?.Invoke();}, error);
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

            PlayerPrefs.SetInt(Const.PLAYER_ID_KEY, playerNetworkData.id);
        }
    }
}
