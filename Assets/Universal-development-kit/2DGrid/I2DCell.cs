using UnityEngine;

namespace CellField2D
{
    public interface I2DCell
    {
        public Vector2Int coordinates { get; }
        public int x { get; }
        public int y { get; }
    }

}
