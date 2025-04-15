using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace OFG.ChessPeak
{
    public class BootstarapInstaller : MonoInstaller
    {
        [Header(H.Components)]
        [SerializeField] private ThemeManager _themeManager;
        [SerializeField] private NetworkManager _networkManager;
        [SerializeField] private JsonToStreamingAssetsStorageService _storageService;
        [SerializeField] private Settings _projectSettings;
        [SerializeField] private AccountManager _accountManager;
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.Bind<ThemeManager>().FromInstance(_themeManager).AsSingle().NonLazy();
            Container.Bind<IStorageService>().FromInstance(_storageService).AsSingle().NonLazy();
            Container.Bind<INetworkManager>().FromInstance(_networkManager).AsSingle().NonLazy();
            Container.Bind<IProjectSettings>().FromInstance(_projectSettings).AsSingle().NonLazy();
            Container.Bind<IAccountManager>().FromInstance(_accountManager).AsSingle().NonLazy();

            #region Signals
            Container.DeclareSignal<SignalInputLoadScene>();
            Container.DeclareSignal<SignalInputLoadCustomLevel>();
            Container.DeclareSignal<SignalInputLoadThemeShop>();
            Container.DeclareSignal<SignalPlayerAccountUpdated>();
            #endregion
        }

        private void Start()
        {
#if UNITY_EDITOR
            SceneManager.LoadScene(0);
#endif
        }
    }
}
