using System;
using System.Collections;
using System.Collections.Generic;
using OFG.ChessPeak.UI;
using Singletones;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OFG.ChessPeak
{
    public sealed class LevelManager : Singletone<LevelManager>
    {
        [Header(H.ComponentReferences)]
        [Header(H.Prefabs)]
        [SerializeField] private GameObject _transitionScreenPrefab;

        [Header(H.Params)]
        [SerializeField][Min(0.0f)] private float _transitionDuration;
        [SerializeField] private int _sceneBuildIndexMainMenu;
        [SerializeField] private int _sceneBuildIndexGame;
        [SerializeField] private int _sceneBuildIndexLevelBuilder;
        [SerializeField] private int _sceneBuildIndexThemeShop;
        [SerializeField] private List<LevelTemplate> _levels;

        private IStorageService _storageService;

        private void Start()
        {
            _storageService = new JsonToStreamingAssetsStorageService();
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
            _storageService.Load<LevelData>("level" + levelNumber, data =>
            {
                _ = StartCoroutine(RoutineLoadingLevel(data, levelNumber));
            });
        }

        private void OnEnable()
        {
            EventBusProvider.EventBus.RegisterCallback<EventInputLoadLevel>(OnInputLoadLevel);
            EventBusProvider.EventBus.RegisterCallback<EventInputLoadMenu>(LoadMainMenu);
            EventBusProvider.EventBus.RegisterCallback<EventInputLoadLevelBuilder>(LoadLevelBuilder);
            EventBusProvider.EventBus.RegisterCallback<EventInputLoadThemeShop>(LoadThemeShop);
            DontDestroyOnLoad(gameObject);
        }

        private void OnDisable()
        {
            EventBusProvider.EventBus.UnregisterCallback<EventInputLoadLevel>(OnInputLoadLevel);
            EventBusProvider.EventBus.UnregisterCallback<EventInputLoadMenu>(LoadMainMenu);
            EventBusProvider.EventBus.UnregisterCallback<EventInputLoadLevelBuilder>(LoadLevelBuilder);
            EventBusProvider.EventBus.UnregisterCallback<EventInputLoadThemeShop>(LoadThemeShop);
        }

        private void OnInputLoadLevel(EventInputLoadLevel context) => 
            LoadLevel(context.LevelNumber);

        private void LoadMainMenu(EventInputLoadMenu context) => 
            StartCoroutine(LoadSceneWithTransition(_sceneBuildIndexMainMenu));
        private void LoadLevelBuilder(EventInputLoadLevelBuilder context) => 
            StartCoroutine(LoadSceneWithTransition(_sceneBuildIndexLevelBuilder));
        private void LoadThemeShop(EventInputLoadThemeShop context) =>
            StartCoroutine(LoadSceneWithTransition(_sceneBuildIndexThemeShop));

        private IEnumerator RoutineLoadingLevel(LevelData levelTemplate, int levelNumber)
        {
            EventLoadLevelComplete context = new(levelTemplate, levelNumber);
            if (IsActiveGameScene)
            {
                yield return TransitionScreen.Show(_transitionDuration);
                EventBusProvider.EventBus.InvokeEvent(context);
                yield return TransitionScreen.Hide(_transitionDuration);
                InvoceTransitionComplete(true);
            }
            else
            {
                yield return LoadSceneWithTransition(_sceneBuildIndexGame, data => { 
                    EventBusProvider.EventBus.InvokeEvent(context);
                });
            }
        }

        private IEnumerator LoadSceneWithTransition(int buildIndex, Action<bool> sceneLoaded = null)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(buildIndex);
            asyncOperation.allowSceneActivation = false;
            yield return TransitionScreen.Show(_transitionDuration);
            yield return WaitLoadingToContinue(asyncOperation);
            asyncOperation.allowSceneActivation = true;
            yield return WaitLoadingIsDone(asyncOperation);
            sceneLoaded?.Invoke(true);
            yield return TransitionScreen.Hide(_transitionDuration);
            InvoceTransitionComplete(true);
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

        private void InvoceTransitionComplete(bool isSucsess)
        {
            if (isSucsess)
            {
                EventTransitionComplete ctx = new EventTransitionComplete();
                EventBusProvider.EventBus.InvokeEvent(ctx);
            }
        }
    }
}
