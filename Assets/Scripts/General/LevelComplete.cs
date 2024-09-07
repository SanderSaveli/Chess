using UnityEngine;

namespace OFG.ChessPeak
{
    public class LevelComplete : MonoBehaviour
    {
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject losePanel;

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
        }

        private void UnsubscribeFromEvents()
        {
            EventBusProvider.EventBus.UnregisterCallback<EventWinning>(LevelCompl);
            EventBusProvider.EventBus.UnregisterCallback<EventLosing>(LevelRestart);
            EventBusProvider.EventBus.UnregisterCallback<EventLoadLevelComplete>(DisableWindows);
        }

        private void LevelCompl(EventWinning ctx)
        {
            winPanel.SetActive(true);
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
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
    }
}
