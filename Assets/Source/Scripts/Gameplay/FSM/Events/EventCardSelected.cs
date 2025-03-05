namespace OFG.ChessPeak
{
    public readonly struct EventCardSelected
    {
        public EventCardSelected(CardType cardType) => CardType = cardType;

        public readonly CardType CardType { get; }
    }
}
