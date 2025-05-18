using Enums;
using Newtonsoft.Json;
using OFG.ChessPeak;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace CustomText
{
    public class TextsManager : MonoBehaviour
    {
        public List<TextTableStruct> TableTexts => _tableTexts;
        public event Action OnLoadedTexts;
        public TypeLocale Locale => _locale;

        [SerializeField] private TypeLocale _locale = TypeLocale.RU;

        private const string URL = "https://script.google.com/macros/s/AKfycbwwXCnY_suUiBHCgrpGtwPNoIhpBAABv-SMe9H4Nns2wM9vRsTTbV4IDLyODxRVjh6I/exec";

        private const string _path = "Text/application_texts";

        private List<TextTableStruct> _tableTexts;
        private IStorageService _storageService;

        private void Awake() => GetTexts();
        private IProjectSettings _settings;

        [Inject]
        public void Construct(IStorageService storageService, IProjectSettings settings)
        {
            _storageService = storageService;
            _settings = settings;
        }

        private void Start()
        {
            SetLocaleOrDefault(_settings.LanguageKey);
        }

        private void GetTexts()
        {
            if (Application.isEditor)
                StartCoroutine(APIServer.GET_BY_URL(URL, SetTexts, Error));
            else
            {
                Debug.Log("TEXTS найден " + _path);
                _storageService.Load<List<TextTableStruct>>(_path, SetTexts);
            }
        }

        private void Error(string s)
        {
            Debug.LogError("Error table texts: " + s);
        }

        private void SetTexts(string data)
        {
            _tableTexts = JsonConvert.DeserializeObject<List<TextTableStruct>>(data);
            SetTexts(_tableTexts);
        }

        private void SetTexts(List<TextTableStruct> texts)
        {
            _tableTexts = texts;
            if (_tableTexts == null)
                Debug.LogError("tableTests is null");
            else
            {
                OnLoadedTexts?.Invoke();
            }
#if UNITY_EDITOR
            try
            {
                _storageService.Save(_path, _tableTexts);
                Debug.Log("TEXTS сохранен в: " + _path);
            }
            catch (Exception e)
            {
                Debug.LogError("Ошибка при сохранении TEXTS: " + e.Message);
            }
#endif
        }

        public void SetLocale(TypeLocale type)
        {
            _locale = type;
            _settings.LanguageKey = type.ToString();
            OnLoadedTexts?.Invoke();
        }

        public void SetLocaleOrDefault(string value)
        {
            if(value == TypeLocale.RU.ToString())
            {
                _locale = TypeLocale.RU;
            }
            else if (value == TypeLocale.EN.ToString())
            {
                _locale = TypeLocale.EN;
            }
            else
            {
                _locale = TypeLocale.EN;
            }
        }

        public string GetTableTextByKey(string key)
        {
            if (TableTexts == null) return String.Empty;
            TextTableStruct textType = TableTexts.FirstOrDefault(t => t.KEY == key);
            if (textType == null) return String.Empty;
            switch (_locale)
            {
                case TypeLocale.EN:
                    return textType.EN;
                case TypeLocale.RU:
                    return textType.RU;
                default:
                    return textType.EN;
            }
        }
    }

    [Serializable]
    public class TextTableStruct
    {
        public string ID;
        public string KEY;
        public string RU;
        public string EN;
    }
}