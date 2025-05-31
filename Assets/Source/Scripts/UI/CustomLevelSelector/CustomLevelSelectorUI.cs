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
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus, INetworkManager networkManager)
        {
            _signalBus = signalBus;
            _networkManager = networkManager;
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

        private void InvokeOnLevelSelectedEvent(int levelName)
        {
            _signalBus.Fire(new SignalInputLoadCustomLevel(levelName));
        }

        private void ShowCustomLevles()
        {
            LevelListContext context = new LevelListContext(_itemsPerPage, _page);
            _networkManager.GetCustomLevelList(context, InitLevelIcons, ErrorGetingLevels);
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

        private void ErrorGetingLevels()
        {
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
    }
}
