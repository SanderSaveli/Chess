using IUP.Toolkit;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace OFG.ChessPeak
{
    [Serializable]
    public class ToolFigurePair
    {
        [SerializeField] private ToolTypes _tool;
        [SerializeField] private GameObject _figurePrefab;

        public ToolTypes Tool { get => _tool; }
        public GameObject Figure { get => _figurePrefab; }
    }

    public class ToolHandler : MonoBehaviour
    {
        [SerializeField] private List<ToolFigurePair> _toolPairsList;

        private GameField _gameField;
        private Tilemap _figureTilemap;
        private EventRegistrar _eventRegistrar;
        private Dictionary<ToolTypes, GameObject> _toolPair;
        private ToolTypes _currentTool;

        public void Init(GameField gameField)
        {
            _gameField = gameField;
            _figureTilemap = gameField.FigureTilemap;
        }

        public void ApplyTool(Vector2Int position2)
        {
            if (_gameField.OutBounds(position2))
                return;

            if (_currentTool == ToolTypes.DeleteFigures)
            {
                if (_gameField.TryGetFigure(out Figure figure, position2))
                {
                    _gameField.Figures[position2] = null;
                    DestroyImmediate(figure.gameObject);
                }
            }
            else
            {
                if (_gameField.TryGetFigure(out _, position2))
                    return;
                CreateFigure(position2);
            }
        }

        private void Awake() => SubscribeOnEvents();

        private void Start()
        {
            CreateToolDictionary();
        }

        private void OnDestroy() => UnsubscribeFromEvents();

        private void OnDisable() => UnsubscribeFromEvents();

        private void SubscribeOnEvents() =>
            _eventRegistrar = new EventRegistrar(EventBusProvider.EventBus)
                .RegisterCallback<EventToolSelected>(OnNewToolSelected);


        private void UnsubscribeFromEvents() => _eventRegistrar.UnregisterAll();

        private void CreateToolDictionary()
        {
            _toolPair = new();
            foreach (var pair in _toolPairsList)
            {
                _toolPair.Add(pair.Tool, pair.Figure);
            }
            Debug.Log(_toolPair.Count);
        }

        private void OnNewToolSelected(EventToolSelected context)
        {
            _currentTool = context.Tool;
        }

        private void CreateFigure(Vector2Int position2)
        {
            GameObject figureObj = _toolPair[_currentTool];
            Vector3 worldPos = _gameField.Position2ToWorld(position2);
            figureObj = Instantiate(figureObj, worldPos, Quaternion.identity);
            figureObj.transform.parent = _figureTilemap.transform;
            Figure figure = figureObj.GetComponent<Figure>();
            _gameField.Figures[position2] = figure;
            figure.View.Create();

            EventFigurePlacedInBuilder context = new EventFigurePlacedInBuilder(position2);
            EventBusProvider.EventBus.InvokeEvent(context);
        }
    }
}
