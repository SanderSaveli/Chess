using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class CustomLevelSelectorUI : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private Transform _levelIconsRoot;

        [Header(H.Prefabs)]
        [SerializeField] private GameObject _levelIconPrefab;

        private List<CustomLevelButtonView> _levelButtonViews = new();
        private List<string> _customLevelsNames = new();

        private void Start() => InitLevelIcons();

        private void OnDestroy() => UnsubscribeFromEvents();

        private void InvokeOnLevelSelectedEvent(string levelName)
        {
            EventInputLoadCustomLevel context = new(levelName);
            EventBusProvider.EventBus.InvokeEvent(context);
        }

        private void InitLevelIcons()
        {
            GetAllCustomLevels();
            for (int i = 0; i < _customLevelsNames.Count; i ++)
            {
                GameObject levelButtonViewObject = Instantiate(_levelIconPrefab, _levelIconsRoot);
                CustomLevelButtonView levelButtonView = levelButtonViewObject.GetComponent<CustomLevelButtonView>();
                levelButtonView.Clicked += InvokeOnLevelSelectedEvent;
                _levelButtonViews.Add(levelButtonView);
                levelButtonView.UpdateView(_customLevelsNames[i]);
            }
        }

        private void UnsubscribeFromEvents()
        {
            foreach (CustomLevelButtonView levelButtonView in _levelButtonViews)
            {
                levelButtonView.Clicked -= InvokeOnLevelSelectedEvent;
            }
        }

        private void GetAllCustomLevels()
        {
            string path = BuildStreamingAssetsPath("CustomLevels/");
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    string fileExtension = Path.GetExtension(file);
                    if (fileExtension != ".meta" && (File.GetAttributes(file) & FileAttributes.Hidden) == 0)
                    {
                        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                        _customLevelsNames.Add(fileNameWithoutExtension);
                    }
                }
            }
            else
            {
                Debug.LogError($"Директория {path} не найдена.");
            }
        }


        private string BuildStreamingAssetsPath(string key)
        {
            return Path.Combine(Application.streamingAssetsPath, key);
        }
    }
}
