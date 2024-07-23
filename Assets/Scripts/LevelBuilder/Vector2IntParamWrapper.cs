using UnityEngine;

namespace OFG.ChessPeak
{
    public class Vector2IntParamWrapper : MonoBehaviour
    {
        [SerializeField] private Vector2Int _vec;

        public Vector2Int Vec => _vec;
    }
}
