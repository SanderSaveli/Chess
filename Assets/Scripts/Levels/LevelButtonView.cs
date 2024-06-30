using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OFG.Chess
{
    [RequireComponent(typeof(Button))]
    public class LevelButtonView : MonoBehaviour
    {
        private int levelNum;
        Action<int> OnLevelClick;
        [SerializeField] Image background;
        [SerializeField] Image lockImage;
        [SerializeField] TMP_Text text;

        private Color _normalColor;

        private void Awake()
        {
            _normalColor = background.color;
        }
        public void Activate(int levelNum, Action<int> OnButtonClicked)
        {
            this.levelNum = levelNum;
            OnLevelClick = OnButtonClicked;
            GetComponent<Button>().onClick.AddListener(Click);
            background.color = _normalColor;
            lockImage.enabled = false;
            text.text = levelNum.ToString();
        }

        private void OnDisable()
        {
            Deactivate();
        }

        private void Click()
        {
            OnLevelClick(levelNum);
        }

        public void Deactivate()
        {
            background.color = Color.grey;
            GetComponent<Button>().onClick.RemoveListener(Click);
            lockImage.enabled = true;
            text.text = "";
        }
    }
}
