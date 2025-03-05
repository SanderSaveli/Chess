using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace OFG.ChessPeak.UI
{
    public sealed class LevelSelectorUI : MonoBehaviour
    {
        [Header(H.Components)]
        [SerializeField] private Transform _levelIconsRoot;

        [Header(H.Prefabs)]
        [SerializeField] private GameObject _levelIconPrefab;

        private readonly List<LevelButtonView> _levelButtonViews = new(PlayerProgress.LevelsCount);
        private DiContainer _diContainer;

        [Inject]
        public void Construct(DiContainer container)
        {
            _diContainer = container;
        }

        private void Start() => InitLevelIcons();

        private void OnDestroy() => UnsubscribeFromEvents();

        private void InvokeOnLevelSelectedEvent(int levelNumber)
        {
            EventInputLoadLevel context = new(levelNumber);
            EventBusProvider.EventBus.InvokeEvent(context);
        }
        private void InitLevelIcons()
        {
            Debug.Log(gameObject.name);
            for (int i = 1; i <= PlayerProgress.LevelsCount; i += 1)
            {
                LevelButtonView levelButtonView = _diContainer.InstantiatePrefabForComponent<LevelButtonView>(_levelIconPrefab, _levelIconsRoot);
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
