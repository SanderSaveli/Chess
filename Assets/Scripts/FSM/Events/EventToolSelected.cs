namespace OFG.ChessPeak
{
    public readonly struct EventToolSelected
    {
        public EventToolSelected(ToolTypes levelNumber) => Tool = levelNumber;

        public readonly ToolTypes Tool { get; }
    }
}
