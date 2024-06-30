using System.Collections.Generic;
using UDK.SceneLoad;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OFG.Chess
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private int NotLevelsSceneCount = 1;
        [SerializeField] private Transform levelButtonsViewParent;
        [SerializeField] private GameObject levelButtonViewPreffab;
        private SceneLoader loader;

        private void Start()
        {
            loader = SceneLoader.instance;
            ActivateViews();
        }
        public void LoadLevel(int index)
        {
            loader.LoadScene("Level" + index, 1);
        }

        private void ActivateViews()
        {
            List<LevelButtonView> buttonViews = CreateViews(SceneManager.sceneCountInBuildSettings - NotLevelsSceneCount);

            int openLevelCount = GetLastOpenLevel();
            for(int i = 1; i <= openLevelCount; i++)
            {
                buttonViews[i - 1].Activate(i, LoadLevel);
            }
            for (int i = openLevelCount+1; i <= SceneManager.sceneCountInBuildSettings - NotLevelsSceneCount; i++)
            {
                buttonViews[i - 1].Deactivate();
            }
        }

        private int GetLastOpenLevel()
        {
            if (!PlayerPrefs.HasKey("Levels"))
            {
                PlayerPrefs.SetInt("Levels", 1);
            }
            return PlayerPrefs.GetInt("Levels");
        }
        private List<LevelButtonView> CreateViews(int count)
        {
            List<LevelButtonView> buttonViews = new List<LevelButtonView>();

            for (int i = 0; i < count; i++)
            {
                GameObject newView = Instantiate(levelButtonViewPreffab, levelButtonsViewParent);
                LevelButtonView buttonView = newView.GetComponent<LevelButtonView>();
                buttonViews.Add(buttonView);
            }

            Debug.Log(buttonViews.Count);
            return buttonViews;
        }
    }
}
