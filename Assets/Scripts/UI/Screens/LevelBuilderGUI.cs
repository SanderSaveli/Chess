using TMPro;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class LevelBuilderGUI : MonoBehaviour
    {
        [SerializeField] private GameObject ConfirmPanel;

        private void Start()
        {
            ConfirmPanel.SetActive(false);
        }

        public void ShowConfirmPanel()
        {
            ConfirmPanel.SetActive(true);
        }

        public void CloseConfirmPanel()
        {
            ConfirmPanel.SetActive(false);
        }
        public void LoadMainMenu()
        {
            EventInputLoadMenu context = new EventInputLoadMenu();
            EventBusProvider.EventBus.InvokeEvent(context);
        }
    }
}
