using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class RadioButtonGroup : MonoBehaviour
    {
        private Dictionary<int, List<RadioButton>> _radioButtonGroups = new();
        void Start()
        {
            RadioButton[] buttons = FindObjectsOfType<RadioButton>();
            foreach (RadioButton radioButton in buttons)
            {
                int groupID = radioButton.GroupID;
                if (!_radioButtonGroups.ContainsKey(groupID))
                {
                    _radioButtonGroups.Add(groupID, new List<RadioButton>());
                }
                _radioButtonGroups[groupID].Add(radioButton);
                radioButton.OnClick += ChangeSelectedButton;
            }
        }

        private void ChangeSelectedButton(RadioButton radioButton)
        {
            foreach (RadioButton button in _radioButtonGroups[radioButton.GroupID])
            {
                if (button.IsSelected && button != radioButton)
                    button.SetSelect(false);
            }
            radioButton.SetSelect(true);
        }

    }
}
