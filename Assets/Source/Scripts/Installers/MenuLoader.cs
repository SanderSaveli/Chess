using System.Collections;
using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    public class MenuLoader : MonoBehaviour
    {

        private ISceneLoader _loader;

        [Inject]
        public void Construct(ISceneLoader loader)
        {
            _loader = loader;
        }

        private void Start()
        {
            StartCoroutine(StartMenu());
        }

        private IEnumerator StartMenu()
        {
            yield return new WaitForSeconds(0.3f);
            _loader.LoadScene(SceneNames.MainMenu);
        }
    }
}
