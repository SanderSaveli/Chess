using TMPro;
using UnityEngine;

namespace OFG.ChessPeak
{
    [RequireComponent(typeof(TMP_InputField))]
    public abstract class ValidationInputField : MonoBehaviour
    {
        [Header(H.Components)]
        [SerializeField] protected TMP_Text _errorText;

        public TMP_InputField Field => _inputField;
        protected TMP_InputField _inputField;

        private void Start()
        {
            _inputField = GetComponent<TMP_InputField>();
            _errorText.gameObject.SetActive(false);
        }

        public abstract bool Validate();

        protected virtual void ShowError(string message)
        {
            _errorText.gameObject.SetActive(true);
            _errorText.text = message;
        }
    }
}
