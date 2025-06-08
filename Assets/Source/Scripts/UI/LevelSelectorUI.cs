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
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(ISceneLoader sceneLoader, ILevelManager levelManager)
        {
            _levelManager = levelManager;
            _sceneLoader = sceneLoader;
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
            SystemLevelGameEndHandler handler = _levelManager.GenerateLevelEndHandler(levelNumber);
            LevelData levelData = _levelManager.GetLevel(levelNumber).GetData();

            _sceneLoader.LoadLevel(levelData, handler);

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
            int currentLvel = PlayerProgress.GetWorldCurrentLevel(_levelManager.CurrentWorld.ID);

            for (int i = 1; i <= _levelManager.CurrentWorld.LevelsList.Count; i++)
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
