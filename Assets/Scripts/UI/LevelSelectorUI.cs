using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak.UI
{
    public sealed class LevelSelectorUI : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private Transform _levelIconsRoot;

        [Header(H.Prefabs)]
        [SerializeField] private GameObject _levelIconPrefab;

        private readonly List<LevelButtonView> _levelButtonViews = new(PlayerProgress.LevelsCount);

        private void Start() => InitLevelIcons();

        private void OnDestroy() => UnsubscribeFromEvents();

        private void InvokeOnLevelSelectedEvent(int levelNumber)
        {
            EventInputLoadLevel context = new(levelNumber);
            EventBusProvider.EventBus.InvokeEvent(context);
        }

        private void InitLevelIcons()
        {
            for (int i = 1; i <= PlayerProgress.LevelsCount; i += 1)
            {
                GameObject levelButtonViewObject = Instantiate(_levelIconPrefab, _levelIconsRoot);
                LevelButtonView levelButtonView = levelButtonViewObject.GetComponent<LevelButtonView>();
                levelButtonView.Clicked += InvokeOnLevelSelectedEvent;
                _levelButtonViews.Add(levelButtonView);
                if (i < PlayerProgress.CurrentLevel)
                {
                    levelButtonView.UpdateView(i, LevelProgress.Complete);
                }
                else if (i == PlayerProgress.CurrentLevel)
                {
                    levelButtonView.UpdateView(i, LevelProgress.Available);
                }
                else
                {
                    levelButtonView.UpdateView(i, LevelProgress.Locked);
                }
            }
        }

        private void UnsubscribeFromEvents()
        {
            foreach (LevelButtonView levelButtonView in _levelButtonViews)
            {
                levelButtonView.Clicked -= InvokeOnLevelSelectedEvent;
            }
        }
    }
}
