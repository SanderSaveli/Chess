using OFG.ChessPeak.LevelBuild;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OFG.ChessPeak
{
    public class LevelBuilderGUI : MonoBehaviour
    {
        [SerializeField] private GameObject _confirmPanel;
        [SerializeField] private FieldCreator _filedCreator;
        [SerializeField] private Slider _filedSizeSlider;

        private void Start()
        {
            _confirmPanel.SetActive(false);
        }
        private void OnEnable()
        {
            _filedSizeSlider.onValueChanged.AddListener(FieldSizeValueChanged);
        }

        private void OnDisable()
        {
            _filedSizeSlider.onValueChanged.RemoveListener(FieldSizeValueChanged);
        }

        public void FieldSizeValueChanged(float fieldSize)
        {
            int roundedFieldSize = Mathf.RoundToInt(fieldSize); 
            Vector2Int sizeVector = new Vector2Int(roundedFieldSize, roundedFieldSize);
            _filedCreator.ChangeFieldSize(sizeVector);
        }

        public void ShowConfirmPanel()
        {
            _confirmPanel.SetActive(true);
        }

        public void CloseConfirmPanel()
        {
            _confirmPanel.SetActive(false);
        }
        public void LoadMainMenu()
        {
            EventInputLoadMenu context = new EventInputLoadMenu();
            EventBusProvider.EventBus.InvokeEvent(context);
        }
    }
}
