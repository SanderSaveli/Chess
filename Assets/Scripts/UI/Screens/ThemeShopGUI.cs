using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class ThemeShopGUI : MonoBehaviour
    {
        public void LoadMenu()
        {
            EventInputLoadMenu ctx = new EventInputLoadMenu();
            EventBusProvider.EventBus.InvokeEvent(ctx);
        }
    }
}
