using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OFG.Chess
{
    public class SliderFromIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject slider;

        public void OnPointerEnter(PointerEventData eventData)
        {
            slider.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            slider.SetActive(false);
        }
    }
}
