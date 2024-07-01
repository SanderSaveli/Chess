using IUP.Toolkit;
using UnityEngine;

namespace OFG.Chess
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
            EventBus.RegisterCallback<EventWinning>(LevelCompl);
            EventBus.RegisterCallback<EventLosing>(LevelRestart);
        }

        private void UnsubscribeFromEvents()
        {
            EventBus.UnregisterCallback<EventWinning>(LevelCompl);
            EventBus.UnregisterCallback<EventLosing>(LevelRestart);
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
