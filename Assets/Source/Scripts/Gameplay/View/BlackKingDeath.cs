using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class BlackKingDeath : MonoBehaviour
    {
        private void OnEnable()
        {
            EventBusProvider.EventBus.RegisterCallback<EventLoadLevelComplete>(HandleLoadLevelComplete);
        }
        private void OnDisable()
        {
            EventBusProvider.EventBus.UnregisterCallback<EventLoadLevelComplete>(HandleLoadLevelComplete);
        }

        private void HandleLoadLevelComplete(EventLoadLevelComplete ctx)
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
