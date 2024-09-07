using System;
using System.Collections;
using System.Collections.Generic;
using OFG.ChessPeak.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OFG.ChessPeak
{
    public sealed class LevelManager : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [Header(H.Prefabs)]
        [SerializeField] private GameObject _transitionScreenPrefab;

        [Header(H.Params)]
        [SerializeField][Min(0.0f)] private float _transitionDuration;
        [SerializeField] private int _sceneBuildIndexMainMenu;
        [SerializeField] private int _sceneBuildIndexGame;
        [SerializeField] private List<LevelTemplate> _levels;

        private IStorageService _storageService;

        private void Start()
        {
            _storageService = new JsonToFileStorageService();
        }

        public bool IsActiveGameScene
        {
            get
            {
                Scene activeScene = SceneManager.GetActiveScene();
                return activeScene.buildIndex == _sceneBuildIndexGame;
            }
        }

        private TransitionScreen TransitionScreen
        {
            get
            {
                if (_transitionScreen == null)
                {
                    GameObject transitionScreenObject = Instantiate(_transitionScreenPrefab, transform);
                    if (!transitionScreenObject.TryGetComponent(out _transitionScreen))
                    {
                        throw new NullReferenceException(
                            $"Prefab {_transitionScreenPrefab} does not contains" +
                            $"{typeof(TransitionScreen)} component.");
                    }
                }
                return _transitionScreen;
            }
        }

        private TransitionScreen _transitionScreen;

        public void LoadLevel(int levelNumber)
        {
            _storageService.Load<LevelData>("custom_level", data =>
            {
                _ = StartCoroutine(RoutineLoadingLevel(data));
            });
        }

        private void Awake()
        {
            EventBusProvider.EventBus.RegisterCallback<EventInputLoadLevel>(OnInputLoadLevel);
            DontDestroyOnLoad(gameObject);
        }

        private void OnInputLoadLevel(EventInputLoadLevel context) => LoadLevel(context.LevelNumber);

        private IEnumerator RoutineLoadingLevel(LevelData levelTemplate)
        {
            EventLoadLevelComplete context = new(levelTemplate);
            if (IsActiveGameScene)
            {
                yield return TransitionScreen.Show(_transitionDuration);
                EventBusProvider.EventBus.InvokeEvent(context);
                yield return TransitionScreen.Hide(_transitionDuration);
            }
            else
            {
                AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_sceneBuildIndexGame);
                asyncOperation.allowSceneActivation = false;
                yield return TransitionScreen.Show(_transitionDuration);
                yield return WaitLoadingToContinue(asyncOperation);
                asyncOperation.allowSceneActivation = true;
                yield return WaitLoadingIsDone(asyncOperation);
                EventBusProvider.EventBus.InvokeEvent(context);
                yield return TransitionScreen.Hide(_transitionDuration);
            }
        }

        private IEnumerator WaitLoadingToContinue(AsyncOperation asyncOperation)
        {
            while (asyncOperation.progress < 0.9f)
            {
                yield return null;
            }
        }

        private IEnumerator WaitLoadingIsDone(AsyncOperation asyncOperation)
        {
            while (!asyncOperation.isDone)
            {
                yield return null;
            }
        }
    }
}
