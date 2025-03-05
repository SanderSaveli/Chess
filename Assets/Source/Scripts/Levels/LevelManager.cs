using System;
using System.Collections;
using System.Collections.Generic;
using OFG.ChessPeak.UI;
using Singletones;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace OFG.ChessPeak
{
    public sealed class LevelManager : MonoBehaviour
    {
        [Header(H.Components)]
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


        private SignalBus _signalBus;
        private DiContainer _diContainer;

        [Inject]
        public void Construct(SignalBus signalBus, DiContainer diContainer)
        {
            _signalBus = signalBus;
            _diContainer = diContainer;
        }

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
                    GameObject transitionScreenObject = _diContainer.InstantiatePrefab(_transitionScreenPrefab, transform);
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

        public void LoadGameLevel(int levelNumber)
        {
            _storageService.Load<LevelData>("Levels/level" + levelNumber, data =>
            {
                LoadGameLevel(data,levelNumber);
            });
        }
        public void LoadGameLevel(LevelData levelData, int levelNumber)
        {
            _ = StartCoroutine(RoutineLoadingLevel(levelData, levelNumber));
        }

        public void LoadCustomLevel(string levelName)
        {
            _storageService.Load<LevelData>("CustomLevels/" + levelName, data =>
            {
                _ = StartCoroutine(RoutineLoadingLevel(data));
            });
        }

        private void OnEnable()
        {
            EventBusProvider.EventBus.RegisterCallback<EventInputLoadLevel>(OnInputLoadLevel);
            EventBusProvider.EventBus.RegisterCallback<EventInputLoadMenu>(LoadMainMenu);
            EventBusProvider.EventBus.RegisterCallback<EventInputLoadLevelBuilder>(LoadLevelBuilder);
            EventBusProvider.EventBus.RegisterCallback<EventInputLoadThemeShop>(LoadThemeShop);
            EventBusProvider.EventBus.RegisterCallback<EventInputLoadCustomLevel>(OnInputLoadCustomLevel);
            EventBusProvider.EventBus.RegisterCallback<EventInputLoadLevelDirectly>(OnInputLoadLevelDirectly);

            _signalBus.Subscribe<SignalInputLoadScene>(HandleInputLoadScene);
        }

        private void OnDisable()
        {
            EventBusProvider.EventBus.UnregisterCallback<EventInputLoadLevel>(OnInputLoadLevel);
            EventBusProvider.EventBus.UnregisterCallback<EventInputLoadMenu>(LoadMainMenu);
            EventBusProvider.EventBus.UnregisterCallback<EventInputLoadLevelBuilder>(LoadLevelBuilder);
            EventBusProvider.EventBus.UnregisterCallback<EventInputLoadThemeShop>(LoadThemeShop);
            EventBusProvider.EventBus.UnregisterCallback<EventInputLoadCustomLevel>(OnInputLoadCustomLevel);
            EventBusProvider.EventBus.UnregisterCallback<EventInputLoadLevelDirectly>(OnInputLoadLevelDirectly);

            _signalBus.Unsubscribe<SignalInputLoadScene>(HandleInputLoadScene);
        }

        private void HandleInputLoadScene(SignalInputLoadScene ctx)
        {
            StartCoroutine(LoadSceneWithTransition(GetSceneIndex(ctx.SceneName)));
        }
        private void OnInputLoadLevelDirectly(EventInputLoadLevelDirectly context) =>
                LoadGameLevel(context.LevelData, context.LevelIndex);
        private void OnInputLoadLevel(EventInputLoadLevel context) => 
            LoadGameLevel(context.LevelNumber);

        private void OnInputLoadCustomLevel(EventInputLoadCustomLevel context) =>
            LoadCustomLevel(context.levelName);

        private void LoadMainMenu(EventInputLoadMenu context) => 
            StartCoroutine(LoadSceneWithTransition(_sceneBuildIndexMainMenu));
        private void LoadLevelBuilder(EventInputLoadLevelBuilder context) => 
            StartCoroutine(LoadSceneWithTransition(_sceneBuildIndexLevelBuilder));
        private void LoadThemeShop(EventInputLoadThemeShop context) =>
            StartCoroutine(LoadSceneWithTransition(_sceneBuildIndexThemeShop));

        private IEnumerator RoutineLoadingLevel(LevelData levelTemplate, int levelNumber = -1)
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

        private int GetSceneIndex(SceneNames name)
        {
            switch (name)
            {
                case SceneNames.InitialScene:
                    return 0;
                case SceneNames.MainMenu:
                    return _sceneBuildIndexMainMenu;
                case SceneNames.Game:
                    return _sceneBuildIndexGame;
                case SceneNames.LevelBuilder:
                    return _sceneBuildIndexLevelBuilder;
                case SceneNames.ThemeShop:
                    return _sceneBuildIndexThemeShop;
                default:
                    return 0;
            }
        }
    }
}
