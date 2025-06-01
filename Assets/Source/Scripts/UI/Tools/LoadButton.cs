using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OFG.ChessPeak
{
    [RequireComponent(typeof(Button))]
    public class LoadButton : MonoBehaviour
    {
        [Header(H.Components)]
        [SerializeField] private GameObject _loadSpin;
        [SerializeField] private TMP_Text _text;

        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            SetLoad(false);
        }

        public void SetLoad(bool isLoad)
        {
            _loadSpin.SetActive(isLoad);
            _text.gameObject.SetActive(!isLoad);
            _button.interactable = !isLoad;
        }
    }
}
