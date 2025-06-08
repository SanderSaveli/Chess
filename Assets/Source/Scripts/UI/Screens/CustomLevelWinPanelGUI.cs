using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class CustomLevelWinPanelGUI : MonoBehaviour
    {
        private CustomLevelContext _levelContext;
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Init(CustomLevelContext context)
        {
            _levelContext = context;
        }

        public void RestartLevel()
        {
            _sceneLoader.RepeatLevel();
        }

        public void RateLevel()
        {

        }

        public void ExitToMenu()
        {
            _sceneLoader.LoadScene(SceneNames.MainMenu);
        }
    }
}
