using IUP.Toolkit;
using UnityEngine;

namespace OFG.ChessPeak
{
    public sealed class MainMenuGUI : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private GameObject _frameMainMenu;
        [SerializeField] private GameObject _frameLevelSelection;

        private UI_TweenPositionAnimation[] _tweensMainMenu;
        private UI_TweenPositionAnimation[] _tweensLevelSelection;

        private void Start()
        {
            //PlayerPrefs.DeleteKey(PrefsKey.CurrentLevel);
        }

        private void Awake()
        {
            _tweensMainMenu = _frameMainMenu.GetComponentsInChildren<UI_TweenPositionAnimation>(true);
            _tweensLevelSelection = _frameLevelSelection.GetComponentsInChildren<UI_TweenPositionAnimation>(true);
        }

        public void ShowSelectLevelFrame()
        {
            foreach (var tween in _tweensMainMenu)
            {
                tween.Hide();
            }
            foreach (var tween in _tweensLevelSelection)
            {
                tween.Show();
            }
        }

        public void ShowMainMenuFrame()
        {
            foreach (var tween in _tweensMainMenu)
            {
                tween.Show();
            }
            foreach (var tween in _tweensLevelSelection)
            {
                tween.Hide();
            }
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void OpenLevelBuild()
        {
            EventInputLoadLevelBuilder context = new EventInputLoadLevelBuilder();
            EventBusProvider.EventBus.InvokeEvent(context);
        }

        public void OpenThemes()
        {
            EventInputLoadThemeShop context = new EventInputLoadThemeShop();
            EventBusProvider.EventBus.InvokeEvent(context);
        }
    }
}
