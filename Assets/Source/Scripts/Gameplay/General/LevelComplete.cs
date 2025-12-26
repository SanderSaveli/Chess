using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class LevelComplete : MonoBehaviour
    {
        [SerializeField] private Transform _windowParent;

        private UIScreen _winScreen;
        private UIScreen _loseScreen;

        private IGameEndManager _gameEndManager;
        private DiContainer _diContainer;

        [Inject]
        public void Construct(DiContainer diContainer, IGameEndManager gameEndManager)
        {
            _gameEndManager = gameEndManager;
            _diContainer = diContainer;
        }

        private void Awake()
        {
            SubscribeOnEvents();
        }

        private void OnEnable()
        {
            SubscribeOnEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void OnDestroy() => UnsubscribeFromEvents();

        private void SubscribeOnEvents()
        {
            EventBusProvider.EventBus.RegisterCallback<EventWinning>(LevelWin);
            EventBusProvider.EventBus.RegisterCallback<EventLosing>(LevelLose);
            EventBusProvider.EventBus.RegisterCallback<EventLoadLevelComplete>(SpawnWindows);
        }

        private void UnsubscribeFromEvents()
        {
            EventBusProvider.EventBus.UnregisterCallback<EventWinning>(LevelWin);
            EventBusProvider.EventBus.UnregisterCallback<EventLosing>(LevelLose);
            EventBusProvider.EventBus.UnregisterCallback<EventLoadLevelComplete>(SpawnWindows);
        }

        private void LevelWin(EventWinning ctx)
        {
            _winScreen.Show();
            _gameEndManager.GameEndHandler.GameEnd(true);
        }

        private void LevelLose(EventLosing ctx)
        {
            _loseScreen.Show();
            _gameEndManager.GameEndHandler.GameEnd(false);
        }

        private void SpawnWindows(EventLoadLevelComplete ctx)
        {
            DeleteScreens();

            _winScreen = CreateScreen(_gameEndManager.GameEndHandler.WinScreenPrefab);
            _loseScreen = CreateScreen(_gameEndManager.GameEndHandler.LoseScreenPrefab);
        }

        private UIScreen CreateScreen(UIScreen screenPrefab)
        {
            UIScreen screen = _diContainer.InstantiatePrefabForComponent<UIScreen>(screenPrefab, _windowParent);
            screen.Hide();
            return screen;
        }
        private void DeleteScreens()
        {
            Debug.Log("Delete");
            if(_winScreen != null)
            {
                Destroy(_winScreen.gameObject);
                _winScreen = null;
            }
            if(_loseScreen != null)
            {
                Destroy(_loseScreen.gameObject);
                _loseScreen = null;
            }
        }
    }
}
