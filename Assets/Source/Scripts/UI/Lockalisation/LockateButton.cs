using CustomText;
using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace OFG.ChessPeak
{
    public class LockateButton : MonoBehaviour
    {
        [SerializeField] private TypeLocale _typeLocale;
        [SerializeField] private Button _button;
        private TextsManager _textManager;

        [Inject]
        public void Construct(TextsManager textsManager)
        {
            _textManager = textsManager;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(ApplyLocate);
        }

        private void OnDisable()
        {
            _button.onClick.AddListener(ApplyLocate);
        }

        private void ApplyLocate()
        {
            Debug.Log(_typeLocale.ToString());
            _textManager.SetLocale( _typeLocale);
        }
    }
}
