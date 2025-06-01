using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace CustomText
{
    [RequireComponent(typeof(TMP_Text))]
    public class TextByTableKey : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField] private TMP_Text _text;
        [Inject] private TextsManager _textManager;

        private void Start()
        {
            if (_textManager == null) ProjectContext.Instance.Container.Inject(this);
            _textManager.OnLoadedTexts += SetText;
            SetText();
        }

        public void SetText(string Key)
        {
            _key = Key;
            SetText();
        }

        private void SetText()
        {
            string text = _textManager.GetTableTextByKey(_key);
            if (text != String.Empty) _text.text = text;
        }

#if UNITY_EDITOR
        private Coroutine _coroutine;

        private void OnValidate()
        {
            _text = GetComponent<TMP_Text>();
            if (_text == null) Debug.LogError("TMP_Text not found in " + gameObject.name);
        }

#endif
        private void OnDestroy()
        {
            if (_textManager == null) return;
            _textManager.OnLoadedTexts -= SetText;
        }
    }
}