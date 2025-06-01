using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class DefaultGameEndHandler : GameEndHandler
    {
        [SerializeField] protected UIScreen _winScreen;
        [SerializeField] protected UIScreen _loseScreen;

        public override void GameEnd(bool isWin)
        {
            if(isWin)
            {
                _winScreen?.Show();
                OnWin();
            }
            else
            {
                _loseScreen?.Show();
                Onlose();
            }
        }

        protected virtual void OnWin() { }
        protected virtual void Onlose() { }
    }
}
