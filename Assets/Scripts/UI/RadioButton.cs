using System;
using UnityEngine;
using UnityEngine.UI;

namespace OFG.ChessPeak
{
    [RequireComponent (typeof(RadioButtonView), typeof(Button))]
    public class RadioButton : MonoBehaviour
    {
        [SerializeField] private int _groupID = 1;
        [SerializeField] private bool _isSelectedAtStart;
        
        private RadioButtonView _view;

        public Action<RadioButton> OnClick;
        public int GroupID { get => _groupID; }

        public bool IsSelected { get; private set; }



        protected void Start()
        {
            _view = GetComponent<RadioButtonView>();
            GetComponent<Button>().onClick.AddListener(Clicked);
            if (_isSelectedAtStart)
            {
                _view.Select();
                IsSelected = true;
            }
        }

        private void Clicked()
        {
            OnClick.Invoke(this);
        }

        public void SetSelect(bool isSelected)
        {
            this.IsSelected = isSelected;
            if (isSelected)
                _view.Select();
            else
                _view.DeSelect();
        }
    }
}
