using OFG.ChessPeak.UI;
using System.Collections.Generic;

namespace OFG.ChessPeak
{
    public class SystemLevelFiller : ItemFiller<SystemLevelSlot, SystemLevelData>
    {
        public List<SystemLevelSlot> Slots => _slots;
    }
}
