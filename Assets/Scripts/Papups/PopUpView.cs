using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace OFG.ChessPeak.Popup
{
    public class PopUpView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text mainText;
        [SerializeField] private float milisecForOneLetterWrite;

        private string allMainText;

        public Action userClicked;


        public void SetTitleText(string text)
        {
            titleText.text = text;
        }

        public void SetMainText(string text)
        {
            allMainText = text;
            StartCoroutine(PlayTextAnuimation(text, mainText));
        }

        

        private IEnumerator PlayTextAnuimation(string text, TMP_Text textView)
        {
            textView.text = "";
            foreach (char c in text)
            {
                textView.text += c;
                yield return new WaitForSeconds(milisecForOneLetterWrite / 100);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(mainText.text != allMainText)
            {
                StopAllCoroutines();
                mainText.text = allMainText;
            }
            else
            {
                userClicked?.Invoke();
            }
        }
    }
}
