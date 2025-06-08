using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class CustomLevelSelectorUI : MonoBehaviour
    {
        public int Page => _page;
        public int TotalPage {  get; private set; }
        public Action OnPageUpdated;

        [Header(H.Components)]
        [SerializeField] private UIScreen _uIScreen;
        [SerializeField] private MainMenuGUI _mainMenu;
        [SerializeField] private CustomLevelFiller _customLevelFiller;

        [Header(H.Params)]
        [SerializeField] private int _itemsPerPage =10;

        private int _page =1;
        private INetworkManager _networkManager;
        private ISceneLoader _sceneLoader;
        private SignalBus _signalBus;

        private bool _isLoadLevel;
        private int _levelId;

        [Inject]
        public void Construct(SignalBus signalBus, INetworkManager networkManager, ISceneLoader sceneLoader)
        {
            _signalBus = signalBus;
            _networkManager = networkManager;
            _sceneLoader = sceneLoader;
        }

        private void OnEnable()
        {
            _uIScreen.OnShowScreen += ShowCustomLevles;
        }
        private void OnDisable()
        {
            _uIScreen.OnShowScreen -= ShowCustomLevles;
            UnsubscribeFromEvents();
        }

        public void ShowNextPage()
        {
            ShowPage(_page + 1);
        }

        public void ShowPreviousPage()
        {
            ShowPage(_page -1);
        }

        public void ShowPage(int pageNumber)
        {
            if(HasPage(pageNumber)) 
            {
                _page = pageNumber;
                ShowCustomLevles();
            }
        }

        public bool HasNextPage() => TotalPage > _page;

        public bool HasPreviousPage() => _page > 1;

        public bool HasPage(int pageNumber) => pageNumber > 0 && pageNumber <= TotalPage;

        private void InvokeOnLevelSelectedEvent(int id)
        {
            if (_isLoadLevel) return;
            _networkManager.GetFullCustomLevelData(id, GetFullLevelData, NetworkError);
            _levelId = id;
            _isLoadLevel = true;
        }

        private void ShowCustomLevles()
        {
            LevelListContext context = new LevelListContext(_itemsPerPage, _page);
            _networkManager.GetCustomLevelList(context, InitLevelIcons, NetworkError);
        }

        private void InitLevelIcons(LevelListNetworkData data)
        {
            _uIScreen.OnShowScreen -= ShowCustomLevles;
            TotalPage = data.total_pages;
            UnsubscribeFromEvents();
            _customLevelFiller.FillItems(data.level_list);
            SubscribeToEvents();
            OnPageUpdated?.Invoke();
        }

        private void NetworkError()
        {
            _isLoadLevel = false;
            _mainMenu.OpenLevelsErrorScreen();
        }

        private void SubscribeToEvents()
        {
            foreach (CustomLevelSlot levelButtonView in _customLevelFiller.Items)
            {
                levelButtonView.Clicked += InvokeOnLevelSelectedEvent;
            }
        }

        private void UnsubscribeFromEvents()
        {
            foreach (CustomLevelSlot levelButtonView in _customLevelFiller.Items)
            {
                levelButtonView.Clicked -= InvokeOnLevelSelectedEvent;
            }
        }

        private void GetFullLevelData(LevlelNetworkData data)
        {
            _isLoadLevel = false;
            CustomLevelContext ctx = new CustomLevelContext(_levelId);
            CustomLevelGameEndHandler handler = new CustomLevelGameEndHandler(ctx);
            _sceneLoader.LoadLevel(data.data, handler);
        }
    }
}

