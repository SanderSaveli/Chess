using CellField2D;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum FigureColor{
    White,
    Black
}
public class Figure : MonoBehaviour
{
    Camera _camera;
    [SerializeField] public FigureColor _color;  
    public ChessCell currentCell { get; set; }
    public Vector2Int fieldCoordinate => currentCell.coordinates;
    private Vector3 prevPos;

    private IDropHandler lastSelectedView;
    private bool isMovable;

    private void Start()
    {
        _camera = Camera.main;
    }

    public virtual void StartDrag()
    {
        isMovable = true;
        SelectFigure();
    }

    public virtual void UpdateDrag()
    {
        Vector3 pos = _camera.ScreenToWorldPoint(Input.mousePosition);
        if (isMovable)
        {
            moveFigure(pos - prevPos);
            if (TryCatchDropHandler(out IDropHandler handler))
            {
                if (lastSelectedView != null)
                {
                    if (handler != lastSelectedView)
                    {
                        lastSelectedView.ExitHover(this);
                        lastSelectedView = handler;
                    }
                    handler.EnterHover(this);
                }
                else
                {
                    lastSelectedView = handler;
                }
            }
            else
            {
                if (currentCell != null)
                {
                    lastSelectedView.ExitHover(this);
                }
            }
        }
        prevPos = pos;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
    }

    public virtual void EndDrag()
    {
        isMovable = false;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray.origin, ray.direction);
        bool flag = false;
        DeSelectFigure();
        if (TryCatchDropHandler(out IDropHandler handler))
        {
            flag = handler.DropFigure(this);
        }
        if (!flag)
        {
            if (currentCell != null)
            {
                currentCell.DropFigure(this);
            }
        }
        currentCell.DeSelect();
    }

    public virtual void EnterHover()
    {
        HowerOnFigure();
    }

    public virtual void ExitHover()
    {
        DeSelectFigure();
    }

    private void OnMouseDown()
    {
        //if (GameManager.canMoveTiles)
        StartDrag();
    }

    private void OnMouseUp()
    {
       // if (GameManager.canMoveTiles)
       EndDrag();
    }

    private void Update()
    {
        //if (GameManager.canMoveTiles)
        UpdateDrag();
    }

    private bool TryCatchDropHandler(out IDropHandler handler)
    {
        handler = null;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray.origin, ray.direction);
        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.gameObject.TryGetComponent<IDropHandler>(out IDropHandler h))
            {
                handler = h;
                return true;
            }
        }
        return false;
    }

    public void moveFigure(Vector3 deltaMove)
    {
        deltaMove.y = 0;
        transform.position += deltaMove;
    }

    public void SelectFigure()
    {
        transform.position += new Vector3(0,1,0);
        currentCell.gameField.Select(this);
    }

    public void DeSelectFigure()
    {
        transform.position -= new Vector3(0, 1, 0);
        currentCell.gameField.DeSelect(this);
    }

    public void HowerOnFigure()
    {
        Vector3 pos = transform.position;
        pos.y = 1.5f;
        transform.position = pos;
    }

    public virtual List<IReferedCell> GetMoves(GameField field)
    {
        return new List<IReferedCell>();
    }
}
