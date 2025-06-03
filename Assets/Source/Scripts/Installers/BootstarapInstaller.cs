using CustomText;
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
        [SerializeField] private TextsManager _textsManager;
        [SerializeField] private GameContextHolder _gameEnd;
        [SerializeField] private SceneLoader _scemeLoader;
        [SerializeField] private TutorialManager _tutorialManager;
        [SerializeField] private LevelManager _levelManager;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.Bind<ThemeManager>().FromInstance(_themeManager).AsSingle().NonLazy();
            Container.Bind<IStorageService>().FromInstance(_storageService).AsSingle().NonLazy();
            Container.Bind<INetworkManager>().FromInstance(_networkManager).AsSingle().NonLazy();
            Container.Bind<IProjectSettings>().FromInstance(_projectSettings).AsSingle().NonLazy();
            Container.Bind<IAccountManager>().FromInstance(_accountManager).AsSingle().NonLazy();
            Container.Bind<TextsManager>().FromInstance(_textsManager).AsSingle().NonLazy();
            Container.Bind<GameContextHolder>().FromInstance(_gameEnd).AsSingle().NonLazy();
            Container.Bind<ISceneLoader>().FromInstance(_scemeLoader).AsSingle().NonLazy();
            Container.Bind<ITutorialManager>().FromInstance(_tutorialManager).AsSingle().NonLazy();
            Container.Bind<ILevelManager>().FromInstance(_levelManager).AsSingle().NonLazy();

            #region Signals
            Container.DeclareSignal<SignalInputLoadScene>();
            Container.DeclareSignal<SignalInputLoadCustomLevel>();
            Container.DeclareSignal<SignalInputLoadThemeShop>();
            Container.DeclareSignal<SignalPlayerAccountUpdated>();
            Container.DeclareSignal<SignalInputOpenWindow>();
            Container.DeclareSignal<SignalLoadSystemLevel>();
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
