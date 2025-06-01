using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class ItemFiller<TSlot, T> : MonoBehaviour where TSlot : MonoBehaviour, ISlot<T>
    {
        [Header(H.Components)] [SerializeField]
        protected RectTransform _contentPatent;

        [Header(H.Prefabs)] [SerializeField] protected TSlot _content;

        protected DiContainer _container;
        protected List<TSlot> _slots = new List<TSlot>();

        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }

        public virtual void FillItems(List<T> items, Action loader = null, bool clear = false)
        {
            if (items == null || items.Count == 0)
            {
                loader?.Invoke();
                return;
            }

            if (clear)
            {
                _slots.Clear();
                foreach (RectTransform child in _contentPatent) Destroy(child.gameObject);
            }

            int i = 0;
            foreach (T item in items)
            {
                TSlot slot;

                if (_slots.Count > i)
                {
                    slot = _slots[i];
                }
                else
                {
                    slot = CreateSlot(item);
                    SlotCreated(slot);
                    _slots.Add(slot);
                }


                slot.Fill(item);
                i++;
            }

            if (_contentPatent.childCount > 0 && !clear)
                for (; i < _contentPatent.childCount; i++)
                {
                    TSlot destroySlot = _slots[i];
                    _slots.RemoveAt(i);
                    DestroyImmediate(destroySlot.gameObject);
                    i--;
                }

            ScrollBegin();
            if (gameObject.activeInHierarchy)
                StartCoroutine(Delay(loader));
            else
                loader?.Invoke();
        }

        private IEnumerator Delay(Action loader)
        {
            yield return new WaitForSeconds(0.5f);
            loader?.Invoke();
        }

        public virtual void SlotCreated(TSlot slot)
        {
        }

        public virtual void ScrollBegin() => _contentPatent.anchoredPosition = Vector2.zero;


        public virtual TSlot CreateSlot(T item)
        {
            return _container.InstantiatePrefabForComponent<TSlot>(_content, _contentPatent);
        }
    }
}