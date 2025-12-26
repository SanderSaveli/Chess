using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class SystemLevelGameEndHandler : GameEndHandler
    {
        private SystemLevelGameEndContext _context;
        private SignalBus _signalBus;

        public SystemLevelGameEndHandler(SystemLevelGameEndContext context, SignalBus signalBus)
        {
            _context = context;
            _signalBus = signalBus;
        }

        protected override void OnWin()
        {
            UnlockNextLevel();
        }

        private void UnlockNextLevel()
        {
            _context.LevelManager.CompleteLevel(_context.LevelNumber);
        }
        public override void LoadComplete()
        {
            _signalBus.Fire(new SignalOpenSystemLevel(_context));
        }
    }
}
