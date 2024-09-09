using OFG.ChessPeak.LevelBuild;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OFG.ChessPeak
{
    public class FigureScrollElement : ScrollElement
    {
        [SerializeField] private float scaleFactor = 1.2f;
        [SerializeField] private Color selectedColor = Color.white;
        [SerializeField] private float startOffset = 50f;
        [SerializeField] private float showTime = 0.6f;
        [SerializeField] private ToolTypes toolType;
        [SerializeField] private TMP_Text text;

        private Image image;
        private Vector3 defaultScale;
        private Color defaultColor;

        public override void Ini(int index)
        {
            base.Ini(index);
            index += 1;
            StartCoroutine(Show(index*0.2f));
            text.text = toolType.ToString();
        }

        public IEnumerator Show(float Delay)
        {
            float timer = Delay;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                Color color = image.color;
                color.a = 0;
                image.color = color;
                yield return null;
            }
            transform.position -= new Vector3(startOffset, 0,0);
            timer = showTime;

            while(timer > 0)
            {
                timer -= Time.deltaTime;
                Color color = image.color;
                float factor = 1 - timer / showTime;
                color.a = factor;
                image.color = color;
                transform.position += new Vector3(startOffset * (Time.deltaTime / showTime), 0, 0);
                yield return null;
            }
        }

        private void OnEnable()
        {
            image = GetComponent<Image>();
            defaultScale = transform.localScale;
            defaultColor = image.color;
        }
        public override void Deselect()
        {
            transform.position -= new Vector3(40, 0, 0);
            transform.localScale = defaultScale;
            image.color = defaultColor;
        }

        public override void Select()
        {
            transform.localScale = defaultScale * scaleFactor;
            Color col = selectedColor;
            col.a = image.color.a;
            image.color = col;
            transform.position += new Vector3(40, 0, 0);

            EventToolSelected context = new EventToolSelected(toolType);
            EventBusProvider.EventBus.InvokeEvent(context);
        }
    }
}
