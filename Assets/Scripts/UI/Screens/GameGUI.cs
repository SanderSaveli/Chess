using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class GameGUI : MonoBehaviour
    {
        public void ExitToMenu()
        {
            EventInputLoadMenu context = new EventInputLoadMenu();
            EventBusProvider.EventBus.InvokeEvent(context);
        }

        public void NextLevel()
        {
            int levelNumber = PlayerPrefs.GetInt("Level");
            EventInputLoadLevel context = new(levelNumber);
            EventBusProvider.EventBus.InvokeEvent(context);
        }
    }
}
