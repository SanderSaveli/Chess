using UnityEngine;

namespace OFG.ChessPeak
{
    public class ToolkitButtons : MonoBehaviour
    {
        public void ToolSelected(ToolEnumWrapper enumWrapper)
        {
            EventToolSelected context = new EventToolSelected(enumWrapper.ToolType);
            EventBusProvider.EventBus.InvokeEvent(context);
            Debug.Log(context.Tool);
        }

    }
}
