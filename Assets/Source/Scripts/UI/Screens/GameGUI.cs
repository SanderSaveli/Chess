using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class GameGUI : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void ExitToMenu()
        {
            _sceneLoader.LoadScene(SceneNames.MainMenu);
        }

        public void ReplayThisLevel()
        {
            _sceneLoader.RepeatLevel();
        }
    }
}
