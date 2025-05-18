using System;

namespace OFG.ChessPeak
{
    public abstract class BaceGameEndContext
    {
        public Type gameEndType { get; private set; }

        public BaceGameEndContext (Type gameEndType)
        {
            this.gameEndType = gameEndType;
        }
    }
}
