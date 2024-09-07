using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class FigurePlacer : MonoBehaviour
    {
        [SerializeField] private FigurePrefabs _figurePrefabs;
        [SerializeField] private Transform _firueRoot;
        public void ArrangeFigures(List<FigureData> figures, GameField field)
        {
            float delay = 1.1f;
            int index = 0;
            foreach (var figure in figures)
            {
                placeFigure(figure, field, delay + index * 0.15f);
                index ++;
            }
        }

        private void placeFigure(FigureData data, GameField field, float delay)
        {
            GameObject figurePrefab = _figurePrefabs.GetFigurePrefab(data.type, data.color);
            Transform root = _firueRoot.GetChild(0).GetChild(0).GetChild(1);
            GameObject figureObj = Instantiate(figurePrefab, root);
            Figure figure = figureObj.GetComponent<Figure>();

            field.Figures[data.pos] = figure;
            figureObj.transform.position = field.Position2ToWorld(data.pos);
            Debug.Log(data.pos + " " + field.Position2ToWorld(data.pos));
            figure.View.Create(delay);
        }
    }
}
