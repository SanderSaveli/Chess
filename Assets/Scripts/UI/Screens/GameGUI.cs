using UnityEngine;

namespace OFG.ChessPeak
{
    public class GameGUI : MonoBehaviour
    {
        private int _levelIndex;
        private void OnEnable()
        {
            EventBusProvider.EventBus.RegisterCallback<EventLoadLevelComplete>(SetLevelIndex);
        }
        private void SetLevelIndex(EventLoadLevelComplete ctx) => _levelIndex = ctx.LevelNumber;
        public void ExitToMenu()
        {
            EventInputLoadMenu context = new EventInputLoadMenu();
            EventBusProvider.EventBus.InvokeEvent(context);
        }

        public void NextLevel()
        {
            EventInputLoadLevel context = new(_levelIndex + 1);
            EventBusProvider.EventBus.InvokeEvent(context);
        }

        public void ReplayThisLevel()
        {
            EventInputLoadLevel context = new(_levelIndex);
            EventBusProvider.EventBus.InvokeEvent(context);
        }
    }
}
