using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OFG.ChessPeak
{
    [Serializable]
    public class LevelTutorial
    {
        [SerializeField] private int _levelNumber;
        [SerializeField] private TutorialSO _tutorial;

        [HideInInspector]
        public string Name;
        public int LevelNumber => _levelNumber;
        public TutorialSO Tutorial => _tutorial;
    }

    public class TutorialManager : MonoBehaviour, ITutorialManager
    {
        [SerializeField] private List<LevelTutorial> _levelsTutorials;

        public TutorialSO GetTutorial(int levelNumber)
        {
            return _levelsTutorials.FirstOrDefault(t => t.LevelNumber == levelNumber).Tutorial;
        }

        public bool LevlHasTutorial(int levelNumber)
        {
            return _levelsTutorials.Any(t => t.LevelNumber == levelNumber);
        }

        public void CompleteTutorial(int levelNumber)
        {
            PlayerPrefs.SetInt(Const.TUTORIAL_KEY + levelNumber, 1);
        }

        public bool IsNeedToShowTutorial(int levelNumber)
        {
            if(!LevlHasTutorial(levelNumber))
            {
                return false;
            }
            if(PlayerPrefs.HasKey(Const.TUTORIAL_KEY + levelNumber))
            {
                if(PlayerPrefs.GetInt(Const.TUTORIAL_KEY + levelNumber) == 1)
                {
                    return false;
                }
            }
            return true;
        }

        private void OnValidate()
        {
            foreach(var level in _levelsTutorials)
            {
                level.Name = "Level " + level.LevelNumber + " tutorial";
            }
        }
    }
}
