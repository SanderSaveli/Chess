using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OFG.ChessPeak
{
    [Serializable]
    public class ScreensContext
    {
        public LevelType gameEndType;
        public UIScreen winScreen;
        public UIScreen loseScreen;
    }

    public class GameEndManager : MonoBehaviour, IGameEndManager
    {
        public IGameEndHandler GameEndHandler => _gameEndHandler;

        [SerializeField] private List<ScreensContext> _screenContext;
        private IGameEndHandler _gameEndHandler;

        public void SetNewGameEndHandler(IGameEndHandler gameEndHandler)
        {
            gameEndHandler.SetScreenContext(GetScreenContext(gameEndHandler.Type));
            _gameEndHandler = gameEndHandler;
        }

        private ScreensContext GetScreenContext(LevelType type) =>
            _screenContext.FirstOrDefault(t => t.gameEndType == type);
    }
}
