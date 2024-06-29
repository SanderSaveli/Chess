public interface IDropHandler
{
    public bool DropFigure(Figure figure);

    public void EnterHover(Figure figure);
    public void ExitHover(Figure figure);
}
