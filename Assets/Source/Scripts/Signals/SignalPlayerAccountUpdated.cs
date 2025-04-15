namespace OFG.ChessPeak
{
    public readonly struct SignalPlayerAccountUpdated
    {
        public readonly PlayerNetworkData PlayerNetworkData;

        public SignalPlayerAccountUpdated(PlayerNetworkData playerNetworkData)
        {
            PlayerNetworkData = playerNetworkData;
        }
    }
}
