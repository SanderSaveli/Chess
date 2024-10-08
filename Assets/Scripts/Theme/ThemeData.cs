using UnityEngine;

namespace OFG.ChessPeak
{
    [CreateAssetMenu(fileName = "new Theme", menuName = "Themes/Theme")]
    public class ThemeData : ScriptableObject
    {
        [Header("Meta")]
        [SerializeField] private string _name;

        [Header("Background")]
        [SerializeField] private Color _backgroundColor = Color.white;
        [SerializeField] private Color _vignetteColor = Color.white;

        [Header("UI Elements")]
        [SerializeField] private Color _positiveButtonColor = Color.white;
        [SerializeField] private Color _neutralButtonColor = Color.white;
        [SerializeField] private Color _negativeButtonColor = Color.red;
        [SerializeField] private Color _scrollElementUnselected = Color.white;
        [SerializeField] private Color _scrollElementSelected = Color.white;
        [SerializeField] private Color _levelViewCurrentLevel = Color.white;
        [SerializeField] private Color _levelViewCompletedLevel = Color.white;
        [SerializeField] private Color _levelViewLockedLevel =  Color.white;
        [SerializeField] private Sprite _mainMenuImage;
        [SerializeField] private Sprite _deckEditImage;
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

        public string themeName => _name;
        public Color backgroundColor => _backgroundColor;
        public Color vignetteColor => _vignetteColor;
        public Color positiveButtonColor => _positiveButtonColor;
        public Color neutralButtonColor => _neutralButtonColor;
        public Color scrollElementUnselected => _scrollElementUnselected;
        public Color scrollElementSelected => _scrollElementSelected;
        public Color negativeButtonColor => _negativeButtonColor;
        public Color levelViewCurrentLevel => _levelViewCurrentLevel;
        public Color levelViewCompletedLevel => _levelViewCompletedLevel;
        public Color levelViewLockedLevel => _levelViewLockedLevel;
        public Sprite mainMenuImage => _mainMenuImage;
        public Sprite deckEditImage => _deckEditImage;
        public Material mascMaterial => _mascMaterial;
        public Material deckMaterial => _deckMaterial;
        public Material mountainsMaterial => _mountainsMaterial;
        public Material whiteCellMaterial => _whiteCellMaterial;
        public Material blackCellMaterial => _blackCellMaterial;
        public FigureSet figureSet => _figureSet;
        public CardSet cardSet => _cardSet;
    }
}
