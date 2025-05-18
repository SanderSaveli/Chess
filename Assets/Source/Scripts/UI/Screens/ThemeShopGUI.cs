using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class ThemeShopGUI : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void LoadMenuScene()
        {
            _sceneLoader.LoadScene(SceneNames.MainMenu);
        }
    }
}
