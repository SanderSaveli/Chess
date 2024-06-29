using UnityEngine;

namespace CellField2D
{
    public class ReferedCell : OwnedCell, IReferedCell
    {
        public ReferedCell(Vector2Int coordinates, int ownerID, ChessCell cellView) : base(coordinates, ownerID)
        {
            this.cellView = cellView;
        }

        public ChessCell cellView { get; private set; }

        public Figure figure { get; private set; }

        public override void changeOwner(int ownerID)
        {
            if (ownerID == 0)
            {
                figure = null;
            }
            base.changeOwner(ownerID);
            cellView.ChangeOvner(ownerID);

        }

        public void changeOwner(int ownerID, Figure figure)
        {
            this.figure = figure;
            base.changeOwner(ownerID);
            cellView.ChangeOvner(ownerID);

            Debug.Log(this.figure);
        }
    }


}
