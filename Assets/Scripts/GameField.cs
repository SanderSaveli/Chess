using CellField2D;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameField : MonoBehaviour
{
    [SerializeField] private Tilemap fieldLayer;
    [SerializeField] public Tilemap figuresLayer;

    public RectangleField<IReferedCell> cellField;

    private Dictionary<Vector2Int, ChessCell> cellViews = new();

    private void Start()
    {
        cellField = new RectangleField<IReferedCell>();
        InstantField();
    }

    public bool TryPlaceFigure(Figure figure, IReferedCell cell)
    {
        if (cell == null)
        {
            return false;
        }

        if (figure.GetMoves(this).Contains(cell))
        {
            return true;
        }


        return false;
    }

    public void Select(Figure figure)
    {
        List<IReferedCell> cells = figure.GetMoves(this);
        foreach (IReferedCell cell in cells)
        {
            cellViews[cell.coordinates].HighlightPossibleMove();
        }
    }

    public void DeSelect(Figure figure)
    {
        List<IReferedCell> cells = figure.GetMoves(this);
        foreach (IReferedCell cell in cells)
        {
            cellViews[cell.coordinates].RemovePossibleMove();
        }
    }

    public void PlaceFigure(Figure figure, IReferedCell cell)
    {
        Vector2Int cellPos = cell.coordinates;
        Vector3 pos = figuresLayer.CellToWorld(new Vector3Int(cellPos.x, cellPos.y, 0));
        pos.z += 0.5f;
        pos.x += 0.5f;
        figure.transform.position = pos;
        if (figure.currentCell != null)
        {
            figure.currentCell.cell.changeOwner(0, null);
        }
        cell.changeOwner(1, figure);
        figure.currentCell = cell.cellView;
    }

    public void LeaveCell(Figure figure, ChessCell cell)
    {
        cell.cell.changeOwner(0);
    }

    private void InstantField()
    {
        int childCount = fieldLayer.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            if (fieldLayer.transform.GetChild(i).TryGetComponent(out ChessCell cellView))
            {
                Vector2Int coordinate = (Vector2Int)fieldLayer.WorldToCell(cellView.transform.position);
                cellView.cell = new ReferedCell(coordinate, 0, cellView);
                cellField.InstantCell(cellView.cell);
                cellView.SetCoordinates(coordinate);
                cellView.gameField = this;
                cellViews.Add(coordinate, cellView);
            }
        }

        childCount = figuresLayer.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            if (figuresLayer.transform.GetChild(i).TryGetComponent(out Figure figure))
            {
                Vector3 worldPos = figure.transform.position;
                Vector2Int coordinate = (Vector2Int)fieldLayer.WorldToCell(worldPos);
                cellField.TryGetCell(coordinate, out IReferedCell ownedCell);
                PlaceFigure(figure, ownedCell);
            }
        }
    }
}
