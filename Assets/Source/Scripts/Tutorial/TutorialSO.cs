using System;
using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    [Serializable]
    public class TutorialPopupData
    {
        [SerializeField] private string _title;
        [TextArea]
        [SerializeField] private string _description;

        public string Title => _title;
        public string Description => _description;
    }

    [CreateAssetMenu(fileName = "new Tutorial", menuName = "Configs/Tutorial")]
    public class TutorialSO : ScriptableObject
    {
        [SerializeField] private List<TutorialPopupData> _tutorialPopups;
        public IReadOnlyList<TutorialPopupData> TutorialPopups => _tutorialPopups;
    }
}
