using OFG.ChessPeak.LevelBuild;
using TMPro;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class FigureScrollElement : AnimatedScrollElement
    {
        [SerializeField] private ToolTypes toolType;
        [SerializeField] private TMP_Text text;

        public override void Ini(int index)
        {
            base.Ini(index);
            text.text = toolType.ToString();
        }

        public override void Select()
        {
            base.Select();
            EventToolSelected context = new EventToolSelected(toolType);
            EventBusProvider.EventBus.InvokeEvent(context);
        }
    }
}
