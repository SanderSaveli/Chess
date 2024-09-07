using UnityEngine;

namespace OFG.ChessPeak
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private Transform _figureRoot;

        public Transform figureRoot => _figureRoot;
    }
}
