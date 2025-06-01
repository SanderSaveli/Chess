using UnityEngine;

namespace OFG.ChessPeak
{
    public class LevelComplete : MonoBehaviour
    {
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;

        private int _thisLevelNumber;

        private void Awake()
        {
            SubscribeOnEvents();
            DisableWindows();
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
            EventBusProvider.EventBus.RegisterCallback<EventWinning>(LevelCompl);
            EventBusProvider.EventBus.RegisterCallback<EventLosing>(LevelRestart);
            EventBusProvider.EventBus.RegisterCallback<EventLoadLevelComplete>(DisableWindows);
            EventBusProvider.EventBus.RegisterCallback<EventLoadLevelComplete>(SetThisLevelNumber);
        }

        private void UnsubscribeFromEvents()
        {
            EventBusProvider.EventBus.UnregisterCallback<EventWinning>(LevelCompl);
            EventBusProvider.EventBus.UnregisterCallback<EventLosing>(LevelRestart);
            EventBusProvider.EventBus.UnregisterCallback<EventLoadLevelComplete>(DisableWindows);
            EventBusProvider.EventBus.UnregisterCallback<EventLoadLevelComplete>(SetThisLevelNumber);
        }

        private void LevelCompl(EventWinning ctx)
        {
            winPanel.SetActive(true);
            if(_thisLevelNumber == PlayerProgress.CurrentLevel)
            {
                UnlockNextLevel();
            }
        }

        private void LevelRestart(EventLosing ctx)
        {
            losePanel.SetActive(true);
        }

        private void DisableWindows(EventLoadLevelComplete ctx)
        {
            DisableWindows();
        }

        private void DisableWindows()
        {
            winPanel.SetActive(false);
            losePanel.SetActive(false);
        }

        private void SetThisLevelNumber(EventLoadLevelComplete ctx)
        {
            _thisLevelNumber = ctx.LevelNumber;
        }

        private void UnlockNextLevel()
        {
            int nextLevel = PlayerProgress.CurrentLevel += 1;
            PlayerPrefs.SetInt(PrefsKey.CurrentLevel, nextLevel);
        }
    }
}
