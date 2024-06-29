
namespace CellField2D
{
    public interface IReferedCell : IOwnedCell
    {
        public ChessCell cellView { get;}
        public Figure figure { get;}
        public void changeOwner(int ownerID, Figure figure);
    }

}

