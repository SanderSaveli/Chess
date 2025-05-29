using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace OFG.ChessPeak
{
    public class CustomLevelButtonView : MonoBehaviour, ISlot<BriefLevelNetworkData>
    {
        [Header(H.Components)]
        [SerializeField] private ImageColorByType _background;
        [SerializeField] protected TMP_Text _label;
        [SerializeField] protected TMP_Text _playerName;
        [SerializeField] private Button _button;

        private Color _backgroundColor;

        public string _levelName { get; private set; }
        public int ID { get; private set; }

        public event Action<int> Clicked;

        private ThemeData _themeData;

        [Inject]
        public void Construct(ThemeManager themeManager)
        {
            _themeData = themeManager.actualTheme;
        }
        private void Start()
        {
            SubscribeOnEvents();
        }
        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        public virtual void UpdateView(string levelName)
        {
            SetLevelName(levelName);
        }

        private void SubscribeOnEvents()
        {
            _button.onClick.AddListener(OnClicked);
        }

        private void UnsubscribeFromEvents()
        {
            _button.onClick.RemoveListener(OnClicked);
        }

        private void OnClicked() => Clicked?.Invoke(ID);

        private void SetLevelName(string levelNumber)
        {
            _levelName = levelNumber;
            _label.SetText(_levelName);
        }

        public void Fill(BriefLevelNetworkData value)
        {
            ID = value.id;
            _label.SetText(value.level_name);
            _playerName.text = value.player_name;
        }
    }
}
