using UnityEngine;

namespace CellField2D
{
    public class OwnedCell : IOwnedCell
    {
        public OwnedCell(Vector2Int coordinates, int ownerID)
        {
            this.coordinates = coordinates;
            this.ownerID = ownerID;
        }
        public int ownerID { get; private set; }

        public Vector2Int coordinates { get; private set; }

        public int x { get => coordinates.x; }

        public int y { get => coordinates.y; }

        public event IOwnedCell.OwnerChanged OnOwnerChanged;

        public virtual void changeOwner(int ownerID)
        {
            this.ownerID = ownerID;
            OnOwnerChanged?.Invoke(ownerID);
        }
    }

}

