namespace CellField2D
{
    public interface IOwnedCell : I2DCell
    {
        public int ownerID { get; }

        public delegate void OwnerChanged(int newOwnerID);
        public event OwnerChanged OnOwnerChanged;

        public void changeOwner(int ownerID);
    }
}
