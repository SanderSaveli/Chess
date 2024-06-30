using UnityEngine;

namespace OFG.Chess
{
    public sealed class GameManager : MonoBehaviour
    {
        [Header("Component References:")]
        [SerializeField] private InputFSM _fsm;

        private void Awake()
        {
            SubscribeOnEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeOnEvents()
        {

        }

        private void UnsubscribeFromEvents()
        {

        }
    }
}
