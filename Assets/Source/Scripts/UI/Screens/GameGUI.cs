using UnityEngine;

namespace OFG.ChessPeak
{
    public class GameGUI : MonoBehaviour
    {
        private int _levelIndex;
        private LevelData _levelData;
        private void OnEnable()
        {
            EventBusProvider.EventBus.RegisterCallback<EventLoadLevelComplete>(SetLevelData);
        }
        private void SetLevelData(EventLoadLevelComplete ctx)
        {
            _levelData = ctx.LoadedLevelTemplate;
            _levelIndex = ctx.LevelNumber;
        }
        public void ExitToMenu()
        {
            EventInputLoadMenu context = new EventInputLoadMenu();
            EventBusProvider.EventBus.InvokeEvent(context);
        }

        public void NextLevel()
        {
            if(_levelIndex < 0)
            {
                return;
            }
            EventInputLoadLevel context = new(_levelIndex + 1);
            EventBusProvider.EventBus.InvokeEvent(context);
        }

        public void ReplayThisLevel()
        {
            EventInputLoadLevelDirectly context = new(_levelData, _levelIndex);
            EventBusProvider.EventBus.InvokeEvent(context);
        }
    }
}
