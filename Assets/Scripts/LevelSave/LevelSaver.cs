using IUP.Toolkit;
using OFG.ChessPeak.LevelBuild;
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
        public LevelSaver(GameField field, DeckBuilder deck)
        {
            _gameField = field;
            _deckBuilder = deck;
            _storageService = new JsonToStreamingAssetsStorageService();
        }

        public void SaveGameLevel(string number)
        {
            LevelData data = FillLevelData();
            Debug.Log(number);
            int num = int.Parse("4");  
            string key = "level" + number;
            _storageService.Save(key, data, (isSucsess) =>
            {
                if (isSucsess)
                {
                    Debug.Log("Data saved!");
                }
                else
                {
                    Debug.LogWarning("Data not saved!");
                }
            });
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
