using UnityEngine;

namespace OFG.ChessPeak.LevelBuild
{
    public class ToolEnumWrapper : MonoBehaviour
    {
        [SerializeField] private ToolTypes _toolType;

        public ToolTypes ToolType => _toolType;
    }
}
