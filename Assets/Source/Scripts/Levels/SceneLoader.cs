using OFG.ChessPeak.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace OFG.ChessPeak
{
    public sealed class SceneLoader : MonoBehaviour, ISceneLoader
    {
        [Header(H.Components)]
        [Header(H.Prefabs)]
        [SerializeField] private GameObject _transitionScreenPrefab;

        [Header(H.Params)]
        [SerializeField][Min(0.0f)] private float _transitionDuration;

        [SerializeField] private List<LevelTemplate> _levels;

        public bool IsActiveGameScene
        {
            get
            {
                Scene activeScene = SceneManager.GetActiveScene();
                return activeScene.buildIndex == (int) SceneNames.Game;
            }
        }

        private IStorageService _storageService;
        private INetworkManager _networkManager;

        private SignalBus _signalBus;
        private DiContainer _diContainer;

        [Inject]
        public void Construct(
            SignalBus signalBus,
            DiContainer diContainer,
            IStorageService storageService,
            INetworkManager networkManager)
        {
            _signalBus = signalBus;
            _diContainer = diContainer;
            _storageService = storageService;
            _networkManager = networkManager;
        }
        private void OnEnable()
        {
            EventBusProvider.EventBus.RegisterCallback<EventInputLoadLevel>(OnInputLoadLevel);
            EventBusProvider.EventBus.RegisterCallback<EventInputLoadMenu>(LoadMainMenu);
            EventBusProvider.EventBus.RegisterCallback<EventInputLoadLevelBuilder>(LoadLevelBuilder);
            EventBusProvider.EventBus.RegisterCallback<EventInputLoadLevelDirectly>(OnInputLoadLevelDirectly);

            _signalBus.Subscribe<SignalInputLoadThemeShop>(LoadThemeShop);
            _signalBus.Subscribe<SignalInputLoadCustomLevel>(OnInputLoadCustomLevel);
        }

        private void OnDisable()
        {
            EventBusProvider.EventBus.UnregisterCallback<EventInputLoadLevel>(OnInputLoadLevel);
            EventBusProvider.EventBus.UnregisterCallback<EventInputLoadMenu>(LoadMainMenu);
            EventBusProvider.EventBus.UnregisterCallback<EventInputLoadLevelBuilder>(LoadLevelBuilder);
            EventBusProvider.EventBus.UnregisterCallback<EventInputLoadLevelDirectly>(OnInputLoadLevelDirectly);

            _signalBus.Unsubscribe<SignalInputLoadThemeShop>(LoadThemeShop);
            _signalBus.Unsubscribe<SignalInputLoadCustomLevel>(OnInputLoadCustomLevel);
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
                LoadGameLevelDirectly(data, levelNumber);
            });
        }
        public void LoadGameLevelDirectly(LevelData levelData, int levelNumber)
        {
            _ = StartCoroutine(RoutineLoadingLevel(levelData, levelNumber));
        }

        public void LoadCustomLevel(LevlelNetworkData data)
        {
            StartCoroutine(RoutineLoadingLevel(data.level));
        }

        public void LoadScene(SceneNames scene)
        {
            StartCoroutine(LoadSceneWithTransition(scene));
        }

        private void OnInputLoadLevelDirectly(EventInputLoadLevelDirectly context) =>
                LoadGameLevelDirectly(context.LevelData, context.LevelIndex);
        private void OnInputLoadLevel(EventInputLoadLevel context) =>
            LoadGameLevel(context.LevelNumber);

        private void OnInputLoadCustomLevel(SignalInputLoadCustomLevel context) =>
            _networkManager.GetFullCustomLevelData(context.levelName, LoadCustomLevel, ErrorLoadCustomLevel);

        private void LoadMainMenu(EventInputLoadMenu context) =>
            StartCoroutine(LoadSceneWithTransition(SceneNames.MainMenu));
        private void LoadLevelBuilder(EventInputLoadLevelBuilder context)
        {
            StartCoroutine(LoadSceneWithTransition(SceneNames.LevelBuilder));
        }
        private void LoadThemeShop(SignalInputLoadThemeShop context)
        {
            StartCoroutine(LoadSceneWithTransition(SceneNames.ThemeShop));
        }

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
                yield return LoadSceneWithTransition(SceneNames.Game, data =>
                {
                    EventBusProvider.EventBus.InvokeEvent(context);
                });
            }
        }

        private IEnumerator LoadSceneWithTransition(SceneNames names, Action<bool> sceneLoaded = null)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync((int)names);
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

        private void ErrorLoadCustomLevel()
        {
            Debug.LogError("Error Load Custom level");
        }
    }
}
