using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class SystemLevelLosePanelGUI : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(ISceneLoader loader)
        {
            _sceneLoader = loader;
        }

        public void RestartLevel()
        {
            _sceneLoader.RepeatLevel();
        }
    }
}
