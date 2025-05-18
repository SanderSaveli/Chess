using UnityEngine;

namespace OFG.ChessPeak
{
    public abstract class GameEndHandler: MonoBehaviour
    {
        public abstract void GameEnd(bool isWin);
    }
}
