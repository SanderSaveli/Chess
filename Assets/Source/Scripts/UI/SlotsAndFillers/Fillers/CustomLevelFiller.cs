using System.Collections.Generic;

namespace OFG.ChessPeak
{
    public class CustomLevelFiller : ItemFiller<CustomLevelSlot, BriefLevelNetworkData>
    {
        public List<CustomLevelSlot> Items => _slots;
    }
}
