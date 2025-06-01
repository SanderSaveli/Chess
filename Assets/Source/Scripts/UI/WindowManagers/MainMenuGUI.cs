using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public sealed class MainMenuGUI : MonoBehaviour
    {
        private ISceneLoader _loader;
        private SignalBus _signalBus;
        [SerializeField] private MainMenuWindowManager _mainWindow;

        [Inject]
        public void Construct(SignalBus signalBus, ISceneLoader sceneLoader)
        {
            _signalBus = signalBus;
            _loader = sceneLoader;
        }

        #region Screens
        public void OpenMenuScreen()
        {
            _mainWindow.OnInputOpenWindow(new SignalInputOpenWindow(MenuScreens.menu));
        }
        public void OpenDifficultScreen()
        {
            _mainWindow.OnInputOpenWindow(new SignalInputOpenWindow(MenuScreens.difficult));
        }

        public void OpenSystemLevelScreen()
        {
            _mainWindow.OnInputOpenWindow(new SignalInputOpenWindow(MenuScreens.system_leveles));
        }

        public void OpenCustomLevelScreen()
        {
            _mainWindow.OnInputOpenWindow(new SignalInputOpenWindow(MenuScreens.custom_levels));
        }

        public void OpenSettingsScreen()
        {
            _mainWindow.OnInputOpenWindow(new SignalInputOpenWindow(MenuScreens.settings));
        }

        public void OpenProfileScreen()
        {
            _mainWindow.OnInputOpenWindow(new SignalInputOpenWindow(MenuScreens.profile));
        }

        public void OpenLoginScreen()
        {
            _mainWindow.OnInputOpenWindow(new SignalInputOpenWindow(MenuScreens.login));
        }

        public void OpenRegistrationScreen()
        {
            _mainWindow.OnInputOpenWindow(new SignalInputOpenWindow(MenuScreens.register));
        }

        public void OpenLevelsErrorScreen()
        {
            _mainWindow.OnInputOpenWindow(new SignalInputOpenWindow(MenuScreens.levels_error));
        }

        public void ExitGame()
        {
            Application.Quit();
        }
        #endregion

        #region Scenes
        public void LoadLevelConstructorScene()
        {
            _loader.LoadScene(SceneNames.LevelBuilder);
        }

        public void LoadThemeShopScene()
        {
            _loader.LoadScene(SceneNames.ThemeShop);
        }
        #endregion
    }
}
