using IUP.Toolkit;
using UnityEngine;

namespace OFG.Chess
{
    public sealed class CardController : MonoBehaviour
    {
        [Header(H.ComponentReferences)]
        [SerializeField] private PointerController _pointerController;

        public void OnLeftMouseDown()
        {
            if (_pointerController.TryGetCard(out Card card))
            {
                card.SelectCard();
            }
        }
    }
}
