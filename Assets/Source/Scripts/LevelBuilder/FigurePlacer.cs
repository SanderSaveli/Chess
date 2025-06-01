using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class FigurePlacer : MonoBehaviour
    {
        [SerializeField] private Transform _firueRoot;
        [SerializeField] private float _delayBetwenFiguresSpawn = 0.15f;
        [SerializeField] private float _delayBeforeFigureSpawn = 1.1f;

        private FigureSet _figureSet;
        private ThemeData _themeData;

        [Inject]
        public void Construct(ThemeManager themeManager)
        {
            _themeData = themeManager.actualTheme;
        }
        public void ArrangeFigures(List<FigureData> figures, GameField field)
        {
            int index = 0;
            foreach (var figure in figures)
            {
                float delay = _delayBeforeFigureSpawn + index * _delayBetwenFiguresSpawn;
                placeFigure(figure, field, delay);
                index ++;
            }
        }

        private void placeFigure(FigureData data, GameField field, float delay)
        {
            _figureSet = _themeData.figureSet;
            GameObject figurePrefab = _figureSet.GetFigurePrefab(data.type, data.color);
            Transform root = _firueRoot.GetComponentInChildren<LevelView>().figureRoot;
            GameObject figureObj = Instantiate(figurePrefab, root);
            Figure figure = figureObj.GetComponent<Figure>();

            field.Figures[data.pos] = figure;
            figureObj.transform.position = field.Position2ToWorld(data.pos);
            figure.View.Create(delay);
        }
    }
}
