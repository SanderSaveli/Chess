using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace OFG.ChessPeak.UI
{
    public sealed class LevelSelectorUI : MonoBehaviour
    {
        [Header(H.Components)]
        [SerializeField] private SystemLevelFiller _levelFiller;
        [SerializeField] private UIScreen _screen;
        private ILevelManager _levelManager;

        [Inject]
        public void Construct(ILevelManager levelManager)
        {
            _levelManager = levelManager;
        }
        private void Awake()
        {
            _screen.OnShowScreen += InitLevelIcons;
        }

        private void OnDestroy()
        {
            _screen.OnShowScreen -= InitLevelIcons;
            UnsubscribeFromEvents();
        }

        private void InvokeOnLevelSelectedEvent(int levelNumber)
        {
            EventInputLoadLevel context = new(levelNumber);
            EventBusProvider.EventBus.InvokeEvent(context);
        }
        private void InitLevelIcons()
        {
            List<SystemLevelData> levelDatas = GetCurrentWorldLevelsData();

            UnsubscribeFromEvents();
            _levelFiller.FillItems(levelDatas);
            SubscribeToEvents();
        }

        private List<SystemLevelData> GetCurrentWorldLevelsData()
        {
            List<SystemLevelData> levelDatas = new List<SystemLevelData>();
            int currentLvel = PlayerProgress.GetWorldCurrentLevel(_levelManager.CurrentLevel.ID);

            for (int i = 1; i <= _levelManager.CurrentLevel.LevelsList.Count; i++)
            {
                SystemLevelData levelData = new SystemLevelData(i, LevelProgress.Locked);
                if (i < currentLvel)
                {
                    levelData.State = LevelProgress.Complete;
                }
                else if (i == currentLvel)
                {
                    levelData.State = LevelProgress.Available;
                }
                levelDatas.Add(levelData);
            }
            return levelDatas;
        }

        private void SubscribeToEvents()
        {
            foreach (SystemLevelSlot levelButtonView in _levelFiller.Slots)
            {
                levelButtonView.Clicked += InvokeOnLevelSelectedEvent;
            }
        }

        private void UnsubscribeFromEvents()
        {
            foreach (SystemLevelSlot levelButtonView in _levelFiller.Slots)
            {
                levelButtonView.Clicked -= InvokeOnLevelSelectedEvent;
            }
        }
    }
}
