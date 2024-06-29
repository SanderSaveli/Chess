using CellField2D;
using UnityEngine;
using UnityEngine.UI;

public class ChessCell : MonoBehaviour, IDropHandler
{
    public GameField gameField;
    public Color defaultColor = Color.gray;
    public Color highlightColor = Color.yellow;

    [ SerializeField] private Image cellHighlight;

    private MeshRenderer _meshRenderer;

    public Vector2Int coordinates { get => cell.coordinates;}
    public IReferedCell cell { get; set; }

    private void Awake()
    {
        defaultColor = GetComponent<MeshRenderer>().material.color;
    }
    private void Start()
    {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        DeSelect();
    }

    public void SetCoordinates(Vector2Int coordinates)
    {
        //this.coordinates = coordinates;
    }
    public bool DropFigure(Figure figure)
    {
        if (gameField.TryPlaceFigure(figure, cell))
        {
            gameField.PlaceFigure(figure, cell);
            return true;
        }
        return false;
    }

    public void figureLeave(Figure figure)
    {
        gameField.LeaveCell(figure, this);
    }

    public void ChangeOvner(int ownerID)
    {
        if (ownerID == 0)
        {
            DeSelect();
        }
    }

    public void EnterHover(Figure figure)
    {
        Select();
    }

    public void ExitHover(Figure figure)
    {
        DeSelect();
    }

    public void Select()
    {
        _meshRenderer.materials[0].color = highlightColor;
    }

    public void DeSelect()
    {
        _meshRenderer.materials[0].color = defaultColor;
    }

    public void HighlightPossibleMove()
    {
        cellHighlight.color += new Color(0,0,0, 0.3f);
    }

    public void RemovePossibleMove()
    {
        cellHighlight.color -= new Color(0, 0, 0, 0.3f);
    }
}
