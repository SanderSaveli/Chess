using UnityEngine;

namespace OFG.ChessPeak
{
    public class ToolEnumWrapper : MonoBehaviour
    {
        [SerializeField] private ToolTypes _toolType;

        public ToolTypes ToolType => _toolType;
    }
}
