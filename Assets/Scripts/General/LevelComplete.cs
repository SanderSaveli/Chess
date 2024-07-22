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
            winPanel.SetActive(false);
            losePanel.SetActive(false);
        }

        private void OnDestroy() => UnsubscribeFromEvents();

        private void SubscribeOnEvents()
        {
            EventBusProvider.EventBus.RegisterCallback<EventWinning>(LevelCompl);
            EventBusProvider.EventBus.RegisterCallback<EventLosing>(LevelRestart);
        }

        private void UnsubscribeFromEvents()
        {
            EventBusProvider.EventBus.UnregisterCallback<EventWinning>(LevelCompl);
            EventBusProvider.EventBus.UnregisterCallback<EventLosing>(LevelRestart);
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
    }
}
