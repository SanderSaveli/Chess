using UDK.SceneLoad;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OFG.Chess
{
    public class GameButtons : MonoBehaviour
    {
        private SceneLoader loader;

        private void Start()
        {
            loader = SceneLoader.instance;
        }
        public void Restart()
        {
            loader.LoadScene("Level" + (SceneManager.GetActiveScene().buildIndex - 1), 0);
        }

        public void NextLevel()
        {
            Debug.Log("Level" + (SceneManager.GetActiveScene().buildIndex));
            loader.LoadScene("Level" + (SceneManager.GetActiveScene().buildIndex), 0);
        }

        public void MainMenu()
        {
            loader.LoadScene("MainMenu", 0);
        }
    }
}
