using IUP.Toolkit;
using OFG.ChessPeak.LevelBuild;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class LevelSaver
    {
        private GameField _gameField;
        private DeckBuilder _deckBuilder;
        private IStorageService _storageService;

        private const string editorPositionKey = "Editor/LastPosition"; 
        public LevelSaver(GameField field, DeckBuilder deck)
        {
            _gameField = field;
            _deckBuilder = deck;
            _storageService = new JsonToStreamingAssetsStorageService();
        }

        public void SaveEditorState()
        {
            LevelData data = FillLevelData();
            _storageService.Save(editorPositionKey, data, (isSucsess) =>
            {
                if (isSucsess)
                    Debug.Log("Editor state saved!");
                else
                    Debug.LogWarning("Data not saved!");
            });
        }

        public void TryGetLastSave(Action<LevelData> callback)
        {
            LevelData data = FillLevelData();
            _storageService.Load<LevelData>(editorPositionKey, callback);
        }

        public void SaveGameLevel(string number, Action<bool> isSucsess = null)
        {
            LevelData data = FillLevelData();
            string key = "Levels/level" + number;
            _storageService.Save(key, data, isSucsess);
        }

        public void SaveCustomLevel(string name, Action<bool> isSucsess = null)
        {
            LevelData data = FillLevelData();
            string key = "CustomLevels/" + name;
            _storageService.Save(key, data, isSucsess);
        }

        private LevelData FillLevelData()
        {
            LevelData data = new LevelData();

            data.FieldWidth = _gameField.width;
            data.FieldHeight = _gameField.height;

            data.CardsInHand = _deckBuilder.CardsInHand.ToArray();
            data.CardsInDeck = _deckBuilder.CardsInDeck.ToArray();

            data.Figures = GetFiguresData();

            data.Cells = GetCellsData();

            return data;
        }

        private List<FigureData> GetFiguresData()
        {
            List<FigureData> data = new List<FigureData>();
            Matrix<Figure> figures = _gameField.Figures;
            for (int x = 0; x < _gameField.width; x++)
            {
                for (int y = 0; y < _gameField.height; y++)
                {
                    Vector2Int pos = new Vector2Int(x, y);
                    Figure figure = figures[pos];
                    if (figure != null)
                    {
                        data.Add(new FigureData(pos, figure.FigureType, figure.FigureColor));
                    }
                }
            }
            return data;

        }

        private List<CellData> GetCellsData()
        {
            List<CellData> data = new List<CellData>();
            return data;

        }
    }
}
