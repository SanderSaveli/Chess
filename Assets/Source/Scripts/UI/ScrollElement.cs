using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace OFG.ChessPeak
{
    public abstract class ScrollElement : MonoBehaviour, IPointerClickHandler
    {
        public Action<int> OnClicked;
        public RectTransform rectTransform => _rectTransform;

        protected int _index;
        private RectTransform _rectTransform;
        public virtual void Ini(int index)
        {
            _rectTransform = GetComponent<RectTransform>();
            _index = index;
        }
        abstract public void Select();

        abstract public void Deselect();

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke(_index);
        }
    }
}
