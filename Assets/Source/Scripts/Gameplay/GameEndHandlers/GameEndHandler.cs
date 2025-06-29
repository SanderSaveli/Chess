using UnityEngine;

namespace OFG.ChessPeak
{
    public class GameEndHandler : IGameEndHandler
    {
        public UIScreen WinScreenPrefab => _winScreen;
        public UIScreen LoseScreenPrefab => _loseScreen;
        public LevelType Type => _gameEndType;

        [Header(H.Params)]
        [SerializeField] protected LevelType _gameEndType;

        [Header(H.Prefabs)]
        [SerializeField] protected UIScreen _winScreen;
        [SerializeField] protected UIScreen _loseScreen;

        public void SetScreenContext(ScreensContext ctx)
        {
            _winScreen = ctx.winScreen;
            _loseScreen = ctx.loseScreen;
        }

        public void GameEnd(bool isWin)
        {
            if (isWin)
            {
                //_winScreen?.Show();
                OnWin();
            }
            else
            {
                //_loseScreen?.Show();
                Onlose();
            }
        }

        protected virtual void OnWin() { }
        protected virtual void Onlose() { }

        public virtual void LoadComplete()
        {

        }
    }
}
