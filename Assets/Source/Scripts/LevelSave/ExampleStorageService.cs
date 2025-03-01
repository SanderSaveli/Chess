using UnityEngine;

namespace OFG.ChessPeak
{
    public class ExampleStorageService : MonoBehaviour
    {
        private const string key = "custom_level";

        private IStorageService storageService;

        private void Start()
        {
            storageService = new JsonToFileStorageService();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                storageService.Load<LevelData>(key, data =>
                {
                    Debug.Log("Field size: " + data.FieldWidth + " / " + data.FieldHeight);
                    string cardsInHand = "";
                    foreach (var card in data.CardsInHand)
                    {
                        cardsInHand += card.ToString() + ", ";
                    }
                    Debug.Log("Cards in hand: " + cardsInHand);
                    string cardsInDeck = "";
                    foreach (var card in data.CardsInDeck)
                    {
                        cardsInDeck += card.ToString() + ", ";
                    }
                    Debug.Log("Cards in deck: " + cardsInDeck);
                    string cells = "";
                    foreach (var cell in data.Cells)
                    {
                        cells += cell.type.ToString() + " on " + cell.pos.ToString() + ", ";
                    }
                    Debug.Log("Cells: " + cells);

                    string figures = "";
                    foreach (var figure in data.Figures)
                    {
                        figures += figure.color + " " + figure.type.ToString() + " on " + figure.pos.ToString() + ", ";
                    }
                    Debug.Log("Figures: " + figures);
                });
            }
        }
    }
}
