using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace OFG.ChessPeak
{
    public sealed class DeckView : MonoBehaviour
    {
        [Header(H.Components)]
        [SerializeField] private Image _image;

        public IReadOnlyList<CardType> Cards => _cards;

        private readonly List<CardType> _cards = new();
        private ThemeData _themeData;


        [Inject]
        public void Construct(ThemeManager themeManager)
        {
            _themeData = themeManager.actualTheme;
        }

        private void Start()
        {
            SetTheme(_themeData);
        }

        private void OnEnable()
        {
            EventBusProvider.EventBus.RegisterCallback<EventNewThemeSet>(SetTheme);
        }
        private void OnDisable()
        {
            EventBusProvider.EventBus.UnregisterCallback<EventNewThemeSet>(SetTheme);
        }
        private void SetTheme(EventNewThemeSet data) => SetTheme(data.ThemeData);
        private void SetTheme(ThemeData data)
        {
            _image.sprite = data.cardSet.deckcView;
        }

        public bool IsEmpty() => _cards.IsEmpty();
        public bool IsNonEmpty() => _cards.IsNonEmpty();

        public void Init(IEnumerable<CardType> cards)
        {
            _cards.Clear();
            _cards.AddRange(cards);
            UpdateView();
        }

        public bool TryGetCard(out CardType cardType)
        {
            if (_cards.IsNonEmpty())
            {
                cardType = _cards[0];
                _cards.RemoveAt(0);
                UpdateView();
                return true;
            }
            cardType = CardType.None;
            return false;
        }

        private void UpdateView() => _image.enabled = _cards.IsNonEmpty();
    }
}
