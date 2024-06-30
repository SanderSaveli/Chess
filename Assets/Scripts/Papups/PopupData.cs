using UnityEngine;

namespace OFG.Chess.Popup
{
    [CreateAssetMenu(fileName = "new Popup Data", menuName = "Popup/Popup Data")]
    public class PopupData : ScriptableObject
    {
        [SerializeField] private string _titleText;
        [TextArea]
        [SerializeField] private string _mainText;

        public string titleText => _titleText;
        public string mainText => _mainText;
    }
}
