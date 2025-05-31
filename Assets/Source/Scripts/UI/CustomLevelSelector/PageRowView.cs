using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OFG.ChessPeak
{
    public class PageRowView : MonoBehaviour
    {
        [Header(H.Components)]
        [SerializeField] private CustomLevelSelectorUI _levelSelectorUI;
        [Space]
        [SerializeField] private Button _nextPageButton;
        [SerializeField] private Button _previousPageButton;
        [SerializeField] private PageButtonFiller _pageButtonFiller;
        [SerializeField] private GameObject _morePrevious;
        [SerializeField] private GameObject _moreNext;

        [Header(H.Params)]
        [SerializeField] private int _maxButtonsBefore;
        [SerializeField] private int _maxButtonsAfter;

        private void OnEnable()
        {
            _levelSelectorUI.OnPageUpdated += HandleUpdatePage;
        }

        private void OnDisable()
        {
            _levelSelectorUI.OnPageUpdated -= HandleUpdatePage;
        }

        private void HandleUpdatePage()
        {
            int page = _levelSelectorUI.Page;

            _nextPageButton.gameObject.SetActive(_levelSelectorUI.HasNextPage());
            _previousPageButton.gameObject.SetActive(_levelSelectorUI.HasPreviousPage());
            _moreNext.SetActive(_levelSelectorUI.HasPage(page + _maxButtonsAfter +1));
            _morePrevious.SetActive(_levelSelectorUI.HasPage(page - _maxButtonsBefore -1));

            List<PageButtonContext> buttonsContext = new List<PageButtonContext>();
            UnsubscribeFromEvents();
            for (int i = 1; i <= _maxButtonsBefore; i++)
            {
                int checkPage = page - i;
                if (_levelSelectorUI.HasPage(checkPage))
                {
                    buttonsContext.Add(new PageButtonContext(checkPage, false));
                }
                else
                {
                    break;
                }
            }

            buttonsContext.Add( new PageButtonContext( page, true));

            for (int i = 1; i <= _maxButtonsAfter; i++)
            {
                int checkPage = page + i;
                if (_levelSelectorUI.HasPage(checkPage))
                {
                    buttonsContext.Add(new PageButtonContext(checkPage, false));
                }
                else
                {
                    break;
                }
            }
            _pageButtonFiller.FillItems(buttonsContext);
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            foreach(var page in _pageButtonFiller.Buttons)
            {
                page.OnLevelClick += HandlePageButtonClick;
            }
        }

        private void UnsubscribeFromEvents() 
        {
            foreach (var page in _pageButtonFiller.Buttons)
            {
                page.OnLevelClick -= HandlePageButtonClick;
            }
        }

        private void HandlePageButtonClick(int page)
        {
            _levelSelectorUI.ShowPage(page);
        }
    }
}
