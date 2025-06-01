using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OFG.ChessPeak
{
    public class AnimatedGridItemRemover : MonoBehaviour
    {
        [Header("Настройки")]
        public GridLayoutGroup gridLayoutGroup;
        public ContentSizeFitter contentSizeFitter;
        public float scaleDownDuration = 0.3f;
        public float repositionDuration = 0.3f;
        public Ease scaleEase = Ease.InBack;
        public Ease moveEase = Ease.OutCubic;

        private List<RectTransform> _items = new List<RectTransform>();

        private void OnEnable()
        {
            RefreshItemList();
        }

        public void RefreshItemList()
        {
            _items.Clear();
            foreach (Transform child in gridLayoutGroup.transform)
            {
                if (child.gameObject.activeSelf)
                    _items.Add(child as RectTransform);
            }
        }

        public void RemoveItemSmooth(int index)
        {
            RefreshItemList();
            if (index < 0 || index >= _items.Count) return;

            RectTransform itemToRemove = _items[index];
            if (!gameObject.activeInHierarchy)
            {
                _items.RemoveAt(index);
                Destroy(itemToRemove.gameObject);
                return;
            }
            gridLayoutGroup.enabled = false;
            contentSizeFitter.enabled = false;

            List<Vector2> positions = GetAnchoredPositions();

            _items.RemoveAt(index);
            Sequence sequence = DOTween.Sequence();
            sequence.Append(itemToRemove.DOScale(Vector3.one, scaleDownDuration))
                .SetEase(scaleEase)
                .OnComplete(() =>
                {
                    Destroy(itemToRemove.gameObject);
                });
            StartCoroutine(ShiftItemsAfter(index, positions));
        }

        private IEnumerator ShiftItemsAfter(int removedIndex, List<Vector2> previousPositions)
        {
            yield return null; 

            RectTransform containerRect = gridLayoutGroup.GetComponent<RectTransform>();
            float oldHeight = containerRect.rect.height;

            for (int i = removedIndex; i < _items.Count; i++)
            {
                Vector2 targetPos = previousPositions[i];
                _items[i].DOAnchorPos(targetPos, repositionDuration).SetEase(moveEase);
            }

            yield return new WaitForSeconds(repositionDuration);
            gridLayoutGroup.enabled = true;
            contentSizeFitter.enabled = true;

            yield return new WaitForEndOfFrame();
            contentSizeFitter.enabled = false;
            float newHeight = containerRect.rect.height;

            if (!Mathf.Approximately(oldHeight, newHeight))
            {
                // Отключаем фиттер и плавно анимируем высоту вручную
                contentSizeFitter.enabled = false;

                // Устанавливаем старую высоту принудительно
                Vector2 size = containerRect.sizeDelta;
                size.y = oldHeight;
                containerRect.sizeDelta = size;

                // Анимируем до новой высоты
                containerRect.DOSizeDelta(
                    new Vector2(size.x, newHeight),
                    0.3f
                ).SetEase(Ease.OutCubic);
            }

            // Включаем обратно авто-лейаут если нужно
            gridLayoutGroup.enabled = true;
            // contentSizeFitter.enabled = true; // можно вернуть, если нужно
        }


        private List<Vector2> GetAnchoredPositions()
        {
            List<Vector2> positions = new List<Vector2>();
            foreach (var item in _items)
            {
                positions.Add(item.anchoredPosition);
            }
            return positions;
        }
    }
}