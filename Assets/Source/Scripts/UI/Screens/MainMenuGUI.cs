using IUP.Toolkit;
using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public sealed class MainMenuGUI : MonoBehaviour
    {
        [Header(H.Components)]
        [SerializeField] private GameObject _frameMainMenu;
        [SerializeField] private GameObject _frameLevelSelection;
        [SerializeField] private GameObject _frameCustomLevelSelection;

        private UI_TweenPositionAnimation[] _tweensMainMenu;
        private UI_TweenPositionAnimation[] _tweensLevelSelection;
        private UI_TweenPositionAnimation[] _tweensCustomLevelSelection;


        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _tweensMainMenu = _frameMainMenu.GetComponentsInChildren<UI_TweenPositionAnimation>(true);
            _tweensLevelSelection = _frameLevelSelection.GetComponentsInChildren<UI_TweenPositionAnimation>(true);
            _tweensCustomLevelSelection = _frameCustomLevelSelection.GetComponentsInChildren<UI_TweenPositionAnimation>(true);
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

        public void ShowSelectCustomLevelFrame()
        {
            foreach (var tween in _tweensMainMenu)
            {
                tween.Hide();
            }
            foreach (var tween in _tweensCustomLevelSelection)
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
            foreach (var tween in _tweensCustomLevelSelection)
            {
                tween.Hide();
            }
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void OpenLevelBuild()
        {;
            EventInputLoadLevelBuilder context = new EventInputLoadLevelBuilder();
            EventBusProvider.EventBus.InvokeEvent(context);
        }

        public void OpenThemes()
        {
            Debug.Log("Open Themes ");
            _signalBus.Fire(new SignalInputLoadThemeShop());
        }
    }
}
