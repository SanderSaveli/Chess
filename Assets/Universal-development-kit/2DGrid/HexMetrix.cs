using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum HexFlatTopCellDirection
{
    top,
    rightTop,
    rightBottom,
    bottom,
    leftBottom,
    leftTop
}

public enum HexFlatTopVertexDirectios
{
    leftTop,
    rightTop,
    right,
    rightBottom,
    leftBottom,
    left
}

public class HexMetrix 
{
    public static float sideLength = 0.5f;
    public static float radius = 0.43f; //sideLength * math.sin(60 * math.PI / 180);

    public static List<Vector3> GetCoordinatesOfCellVertex(Transform transform)
    {
        List<Vector3> coordinates = new List<Vector3>();
        var position = transform.position;

        coordinates.Add(GetVertex(transform, HexFlatTopVertexDirectios.leftTop));
        coordinates.Add(GetVertex(transform, HexFlatTopVertexDirectios.rightTop));
        coordinates.Add(GetVertex(transform, HexFlatTopVertexDirectios.right));
        coordinates.Add(GetVertex(transform, HexFlatTopVertexDirectios.rightBottom));
        coordinates.Add(GetVertex(transform, HexFlatTopVertexDirectios.leftBottom));
        coordinates.Add(GetVertex(transform, HexFlatTopVertexDirectios.left));

        return coordinates;
    }

    public static List<Vector3> FindCommonVertex(List<Vector3> mainCellDots, List<Vector3> neighbouringCellDots)
    {
        var commonDots = mainCellDots.Intersect(neighbouringCellDots);
        return commonDots.ToList();
    }

    public static Vector3 GetVertex(Transform transform, HexFlatTopVertexDirectios direction)
    {
        Vector3 position = transform.transform.position;
        switch (direction)
        {
            case (HexFlatTopVertexDirectios.leftTop):
                return new Vector3(position.x - 0.5f * sideLength, position.y + radius, -1);
            case (HexFlatTopVertexDirectios.rightTop):
                return new Vector3(position.x + 0.5f * sideLength, position.y + radius, -1);
            case (HexFlatTopVertexDirectios.right):
                return new Vector3(position.x + sideLength, position.y, -1);
            case (HexFlatTopVertexDirectios.rightBottom):
                return new Vector3(position.x + 0.5f * sideLength, position.y - radius, -1);
            case (HexFlatTopVertexDirectios.leftBottom):
                return new Vector3(position.x - 0.5f * sideLength, position.y - radius, -1);
            case (HexFlatTopVertexDirectios.left):
                return new Vector3(position.x - sideLength, position.y, -1);
            default:
                throw new Exception("Incorrect input");
        }
    }

    public static Vector2Int GetCellByDirection(Vector2Int coordinate, HexFlatTopCellDirection direction)
    {
        return coordinate + GetCellNeiborModificator(direction, coordinate);
    }

    public static Vector2Int RotateCellClockwise(Vector2Int center, Vector2Int turned)
    {
        HexFlatTopCellDirection displacement = GetRelativeCellPositionBRelativeToA(center, turned);
        if (displacement == HexFlatTopCellDirection.rightTop) return GetCellByDirection(center, HexFlatTopCellDirection.rightBottom);
        if (displacement == HexFlatTopCellDirection.rightBottom) return GetCellByDirection(center, HexFlatTopCellDirection.bottom);
        if (displacement == HexFlatTopCellDirection.bottom) return GetCellByDirection(center, HexFlatTopCellDirection.leftBottom);
        if (displacement == HexFlatTopCellDirection.leftBottom) return GetCellByDirection(center, HexFlatTopCellDirection.leftTop);
        if (displacement == HexFlatTopCellDirection.leftTop) return GetCellByDirection(center, HexFlatTopCellDirection.top);
        if (displacement == HexFlatTopCellDirection.top) return GetCellByDirection(center, HexFlatTopCellDirection.rightTop);

        return GetCellByDirection(center, HexFlatTopCellDirection.rightBottom);
    }

    public static Vector2Int RotateCellCounterClockwise(Vector2Int center, Vector2Int turned)
    {
        HexFlatTopCellDirection displacement = GetRelativeCellPositionBRelativeToA(center, turned);
        if (displacement == HexFlatTopCellDirection.rightTop) return GetCellByDirection(center, HexFlatTopCellDirection.top);
        if (displacement == HexFlatTopCellDirection.rightBottom) return GetCellByDirection(center, HexFlatTopCellDirection.rightTop);
        if (displacement == HexFlatTopCellDirection.bottom) return GetCellByDirection(center, HexFlatTopCellDirection.rightBottom);
        if (displacement == HexFlatTopCellDirection.leftBottom) return GetCellByDirection(center, HexFlatTopCellDirection.bottom);
        if (displacement == HexFlatTopCellDirection.leftTop) return GetCellByDirection(center, HexFlatTopCellDirection.leftBottom);
        if (displacement == HexFlatTopCellDirection.top) return GetCellByDirection(center, HexFlatTopCellDirection.leftTop);

        return GetCellByDirection(center, HexFlatTopCellDirection.rightBottom);
    }

    public static HexFlatTopCellDirection GetRelativeCellPositionBRelativeToA(Vector2Int a, Vector2Int b)
    {
        Vector2Int displacement = b - a;

        // Define the six possible relative positions
        Vector2Int rightTop = GetCellNeiborModificator(HexFlatTopCellDirection.rightTop, a);
        Vector2Int rightBottom = GetCellNeiborModificator(HexFlatTopCellDirection.rightBottom, a);
        Vector2Int bottom = GetCellNeiborModificator(HexFlatTopCellDirection.bottom, a);
        Vector2Int leftBottom = GetCellNeiborModificator(HexFlatTopCellDirection.leftBottom, a);
        Vector2Int leftTop = GetCellNeiborModificator(HexFlatTopCellDirection.leftTop, a);
        Vector2Int top = GetCellNeiborModificator(HexFlatTopCellDirection.top, a);

        // Check which relative position matches the displacement vector, and return it
        if (displacement == rightTop) return HexFlatTopCellDirection.rightTop;
        if (displacement == rightBottom) return HexFlatTopCellDirection.rightBottom;
        if (displacement == bottom) return HexFlatTopCellDirection.bottom;
        if (displacement == leftBottom) return HexFlatTopCellDirection.leftBottom;
        if (displacement == leftTop) return HexFlatTopCellDirection.leftTop;
        if (displacement == top) return HexFlatTopCellDirection.top;

        throw new Exception("Incorrect Input");
    }

    public static Vector2Int GetCellNeiborModificator(HexFlatTopCellDirection direction, Vector2Int coordinate)
    {
        int x = 0;
        int y = 0;

        switch (direction)
        {
            case HexFlatTopCellDirection.top:
                x++;
                break;
            case HexFlatTopCellDirection.rightTop:
                if (coordinate.y % 2 != 0)
                    x++;
                y++;
                break;
            case HexFlatTopCellDirection.rightBottom:
                x += (coordinate.y % 2 == 0) ? -1 : 0;
                y++;
                break;
            case HexFlatTopCellDirection.bottom:
                x--;
                break;
            case HexFlatTopCellDirection.leftBottom:
                x += (coordinate.y % 2 == 0) ? -1 : 0;
                y--;
                break;
            case HexFlatTopCellDirection.leftTop:
                if (coordinate.y % 2 != 0)
                {
                    x++;
                }
                y--;
                break;
        }

        return new Vector2Int(x, y);
    }

    public static Vector3 GetRightBottomVertex(Vector2Int a, Vector2Int b, Transform cellTransform)
    {
        HexFlatTopCellDirection displacement = GetRelativeCellPositionBRelativeToA(a, b);
        if (displacement == HexFlatTopCellDirection.rightTop) return GetVertex(cellTransform, HexFlatTopVertexDirectios.right);
        if (displacement == HexFlatTopCellDirection.rightBottom) return GetVertex(cellTransform, HexFlatTopVertexDirectios.rightBottom);
        if (displacement == HexFlatTopCellDirection.bottom) return GetVertex(cellTransform, HexFlatTopVertexDirectios.leftBottom);
        if (displacement == HexFlatTopCellDirection.leftBottom) return GetVertex(cellTransform, HexFlatTopVertexDirectios.left);
        if (displacement == HexFlatTopCellDirection.leftTop) return GetVertex(cellTransform, HexFlatTopVertexDirectios.leftTop);
        if (displacement == HexFlatTopCellDirection.top) return GetVertex(cellTransform, HexFlatTopVertexDirectios.rightTop);
        throw new Exception("Incorrect input");
    }
}
