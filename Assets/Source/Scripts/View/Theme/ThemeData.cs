using CustomText;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace OFG.ChessPeak
{
    [CreateAssetMenu(fileName = "new Theme", menuName = "Themes/Theme")]
    public class ThemeData : ScriptableObject
    {
        [Header("Meta")]
        [SerializeField] private string _name;
        [TextArea]
        [SerializeField] private string _desctiption;
        [SerializeField] private int _cost;
        [TextArea]
        [SerializeField] private string _receiptConditions;

        [Header("Colors")]
        [SerializeField] private Color _vineteColor = Color.white;
        [SerializeField] private List<ColorParams> _colors;

        [Header("UI Elements")]
        [SerializeField] private Sprite _mainMenuImage;
        [SerializeField] private Sprite _gameBG;
        [SerializeField] private Sprite _mainMenuPanel;
        [SerializeField] private Sprite _deckEditImage;
        [SerializeField] private Sprite _themeShopBG;
        [SerializeField] private Material _mascMaterial;

        [Header("Field")]
        [SerializeField] private Material _deckMaterial;
        [SerializeField] private Material _mountainsMaterial;
        [SerializeField] private Material _whiteCellMaterial;
        [SerializeField] private Material _blackCellMaterial;

        [Header("Figures")]
        [SerializeField] private FigureSet _figureSet;

        [Header("Cards")]
        [SerializeField] private CardSet _cardSet;

        public string Name => _name;
        public string Description => _desctiption;
        public int Cost => _cost;
        public string ReceiptConditions => _receiptConditions;
        public Color VineteColor = Color.white;
        public List<ColorParams> Colors => _colors;
        public Sprite mainMenuImage => _mainMenuImage;
        public Sprite GameBG => _gameBG;
        public Sprite MainMenuPanel => _mainMenuPanel;
        public Sprite deckEditImage => _deckEditImage;
        public Sprite ThemeShopBG => _themeShopBG;
        public Material mascMaterial => _mascMaterial;
        public Material deckMaterial => _deckMaterial;
        public Material mountainsMaterial => _mountainsMaterial;
        public Material whiteCellMaterial => _whiteCellMaterial;
        public Material blackCellMaterial => _blackCellMaterial;
        public FigureSet figureSet => _figureSet;
        public CardSet cardSet => _cardSet;

        public Color GetColor(Custom_ColorStyle style)
        {
            return _colors.FirstOrDefault(t => t.TextColorType == style).Color;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_colors == null)
                _colors = new List<ColorParams>();

            var enumValues = System.Enum.GetValues(typeof(Custom_ColorStyle)).Cast<Custom_ColorStyle>();

            foreach (var value in enumValues)
            {
                if (!_colors.Any(c => c.TextColorType == value))
                {
                    _colors.Add(new ColorParams
                    {
                        Name = value.ToString(),
                        TextColorType = value,
                        Color = Color.white
                    });
                }
            }

            foreach (var colorParam in _colors)
            {
                colorParam.Name = colorParam.TextColorType.ToString();
            }

            EditorUtility.SetDirty(this);
        }
#endif
    }
}
