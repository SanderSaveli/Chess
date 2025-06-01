using UnityEngine;

namespace OFG.ChessPeak
{
    public class LengthInputField : ValidationInputField
    {
        [Header(H.Params)]
        [Min(0)]
        [SerializeField] private int _minChar;
        [Min(0)]
        [SerializeField] private int _maxChar;

        [Space]
        [SerializeField] private string _shortErrorText = "Слишком короткий пароль";
        [SerializeField] private string _tooLongErrorText = "Слишком длинный пароль";
        public override bool Validate()
        {
            string str = _inputField.text;
            if (str.Length > _maxChar)
            {
                ShowError(_tooLongErrorText);
                return false;
            }
            if (str.Length < _minChar)
            {
                ShowError(_shortErrorText);
                return false;
            }
            _errorText.gameObject.SetActive(false);
            return true;
        }
    }
}
