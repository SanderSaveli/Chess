using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OFG.ChessPeak
{
    public class LevelBuilderGUI : MonoBehaviour
    {
        public void LoadMainMenu()
        {
            EventInputLoadMenu context = new EventInputLoadMenu();
            EventBusProvider.EventBus.InvokeEvent(context);
        }
    }
}
