using UnityEngine;
using UnityEngine.UI;

namespace OFG.ChessPeak
{
    public class ToolkitButtons : MonoBehaviour
    {
        public void ToolSelected(ToolEnumWrapper enumWrapper)
        {
            Debug.Log(enumWrapper.ToolType);
        }

        public void SelectButton(Button button)
        {
            button.Select();
        }
    }
}
