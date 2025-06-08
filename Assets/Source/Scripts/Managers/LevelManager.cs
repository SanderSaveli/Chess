using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using static Cinemachine.DocumentationSortingAttribute;

namespace OFG.ChessPeak
{
    public class LevelManager : MonoBehaviour, ILevelManager
    {
        public IReadOnlyList<LevelListSO> Worlds => _worlds;
        public LevelListSO CurrentWorld => _currentWorld;
        public int CurrentLevel => _currentLevel;

        [SerializeField] private List<LevelListSO> _worlds = new();

        private SignalBus _signalBus;
        private IValuteManager _valuteManager;

        private LevelListSO _currentWorld;
        private int _currentLevel = -1;

        [Inject]
        public void Construct(SignalBus signalBus, IValuteManager valuteManager)
        {
            _signalBus = signalBus;
            _valuteManager = valuteManager;
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<SignalOpenSystemLevel>(HandleLevelActive);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<SignalOpenSystemLevel>(HandleLevelActive);
        }

        private void Start()
        {
            _currentWorld = _worlds[0];
        }

        public void SelectWorld(LevelListSO world)
        {
            _currentWorld = world;
        }

        public SystemLevelSO GetLevel(int number)
        {
            if(number <= _currentWorld.LevelsList.Count)
            {
                return _currentWorld.LevelsList[number - 1];
            }
            else
            {
                return null;
            }
        }

        public SystemLevelGameEndHandler GenerateLevelEndHandler(int levelNumber)
        {
            SystemLevelGameEndContext ctx = new SystemLevelGameEndContext(CurrentWorld.ID, levelNumber, this);
            return new SystemLevelGameEndHandler(ctx, _signalBus);
        }
        public void CompleteLevel(int number)
        {
            int nextLevel = number + 1;
            int lastCompletedLevel = PlayerProgress.GetWorldCurrentLevel(CurrentWorld.ID);

            if (lastCompletedLevel < nextLevel)
            {
                UnlockLevel(nextLevel);
            }
        }

        private void UnlockLevel(int level)
        {
            PlayerProgress.SetWorldCurrentLevel(_currentWorld.ID, level);
            SystemLevelSO so = GetLevel(level);
            if(so != null)
            {
                if(so.Award > 0)
                {
                    _valuteManager.AddValute(so.Award);
                }
            }
        }
        private void HandleLevelActive(SignalOpenSystemLevel ctx)
        {
            _currentLevel = ctx.Ctx.LevelNumber;
        }
    }
}
