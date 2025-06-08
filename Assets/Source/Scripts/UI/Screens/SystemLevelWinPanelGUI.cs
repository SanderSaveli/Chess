using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class SystemLevelWinPanelGUI : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;
        private ILevelManager _levelManager;

        [Inject]
        public void Construct(ISceneLoader loader, ILevelManager levelManager)
        {
            _levelManager = levelManager;
            _sceneLoader = loader;
        }

        public void NextLevel()
        {
            int levelNumber = _levelManager.CurrentLevel + 1;
            if (levelNumber > _levelManager.CurrentWorld.LevelsList.Count)
            {
                _sceneLoader.LoadScene(SceneNames.MainMenu);
            }
            else
            {
                SystemLevelGameEndHandler handler = _levelManager.GenerateLevelEndHandler(levelNumber);
                LevelData levelData = _levelManager.GetLevel(levelNumber).GetData();

                _sceneLoader.LoadLevel(levelData, handler);
            }
            
        }
    }
}
