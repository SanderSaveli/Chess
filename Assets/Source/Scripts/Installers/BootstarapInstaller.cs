using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace OFG.ChessPeak
{
    public class BootstarapInstaller : MonoInstaller
    {
        [Header(H.Components)]
        [SerializeField] private ThemeManager _themeManager;
        [SerializeField] private LockalNetworkManager _networkManager;
        [SerializeField] private JsonToStreamingAssetsStorageService _storageService;
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.Bind<ThemeManager>().FromInstance(_themeManager).AsSingle().NonLazy();
            Container.Bind<IStorageService>().FromInstance(_storageService).AsSingle().NonLazy();
            Container.Bind<INetworkManager>().FromInstance(_networkManager).AsSingle().NonLazy();

            #region Signals
            Container.DeclareSignal<SignalInputLoadScene>();
            Container.DeclareSignal<SignalInputLoadCustomLevel>();
            Container.DeclareSignal<SignalInputLoadThemeShop>();
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
