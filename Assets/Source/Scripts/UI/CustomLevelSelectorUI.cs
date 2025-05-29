using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class CustomLevelSelectorUI : MonoBehaviour
    {
        [Header(H.Components)]
        [SerializeField] private Transform _levelIconsRoot;

        [Header(H.Prefabs)]
        [SerializeField] private GameObject _levelIconPrefab;

        private List<CustomLevelButtonView> _levelButtonViews = new();

        private DiContainer _diContainer;
        private INetworkManager _networkManager;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus, DiContainer diContainer, INetworkManager networkManager)
        {
            _signalBus = signalBus;
            _diContainer = diContainer;
            _networkManager = networkManager;
        }

        private void Start() => ShowCustomLevles();

        private void OnDestroy() => UnsubscribeFromEvents();

        private void InvokeOnLevelSelectedEvent(int levelName)
        {
            _signalBus.Fire(new SignalInputLoadCustomLevel(levelName));
        }

        private void ShowCustomLevles()
        {
            _networkManager.GetCustomLevelList(InitLevelIcons, ErrorGetingLevels);
        }

        private void InitLevelIcons(LevelListNetworkData data)
        {
            for (int i = 0; i < data.level_list.Count; i ++)
            {
                CustomLevelButtonView levelButtonView = _diContainer.InstantiatePrefabForComponent<CustomLevelButtonView>(_levelIconPrefab, _levelIconsRoot);
                levelButtonView.Clicked += InvokeOnLevelSelectedEvent;
                _levelButtonViews.Add(levelButtonView);
                levelButtonView.Fill(data.level_list[i]);
            }
        }

        private void ErrorGetingLevels()
        {

        }

        private void UnsubscribeFromEvents()
        {
            foreach (CustomLevelButtonView levelButtonView in _levelButtonViews)
            {
                levelButtonView.Clicked -= InvokeOnLevelSelectedEvent;
            }
        }
    }
}
