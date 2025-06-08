using Newtonsoft.Json;
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
        private IGameEndManager _gameEndManager;
        private IGameEndHandler _lastEndHandler;
        private LevelData _lastLevelData;


        [Inject]
        public void Construct(IGameEndManager gameEndManager)
        {
            _gameEndManager = gameEndManager;
        }

        public bool IsActiveGameScene
        {
            get
            {
                Scene activeScene = SceneManager.GetActiveScene();
                return activeScene.buildIndex == (int) SceneNames.Game;
            }
        }

        private ILevelManager _levelManager;

        private SignalBus _signalBus;
        private DiContainer _diContainer;

        [Inject]
        public void Construct(
            SignalBus signalBus,
            DiContainer diContainer,
            ILevelManager levelManager)
        {
            _signalBus = signalBus;
            _diContainer = diContainer;
            _levelManager = levelManager;
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

        public void LoadLevel(LevelData levelData, IGameEndHandler handler)
        {
            _gameEndManager.SetNewGameEndHandler(handler);
            _lastLevelData = levelData;
            _lastEndHandler = handler;
            StartCoroutine(RoutineLoadingLevel(levelData, handler.Type));
        }

        public void LoadScene(SceneNames scene)
        {
            StartCoroutine(LoadSceneWithTransition(scene));
        }
        public void RepeatLevel()
        {
            LoadLevel(_lastLevelData, _lastEndHandler);
        }

        private IEnumerator RoutineLoadingLevel(LevelData levelTemplate, LevelType type)
        {
            EventLoadLevelComplete context = new(levelTemplate);
            if (IsActiveGameScene)
            {
                yield return TransitionScreen.Show(_transitionDuration);
                EventBusProvider.EventBus.InvokeEvent(context);
                _gameEndManager.GameEndHandler.LoadComplete();
                yield return TransitionScreen.Hide(_transitionDuration);

                _signalBus.Fire(new SignalLoadLevel(type));
                InvoceTransitionComplete(true);
            }
            else
            {
                yield return LoadSceneWithTransition(SceneNames.Game, data =>
                {
                    EventBusProvider.EventBus.InvokeEvent(context);
                    _gameEndManager.GameEndHandler.LoadComplete();
                    _signalBus.Fire(new SignalLoadLevel(type));
                });
            }
        }

        private IEnumerator LoadSceneWithTransition(SceneNames names, Action<bool> sceneLoaded = null)
        {
            _signalBus.Fire(new SignalStartLoadScene(names));   
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
    }
}
