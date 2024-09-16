using OFG.ChessPeak.LevelBuild;
using TMPro;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class FigureScrollElement : ThemeAnimatedScrollElement
    {
        [SerializeField] private ToolTypes toolType;

        public override void Ini(int index)
        {
            base.Ini(index);
            _text.text = toolType.ToString();
        }

        public override void Ini(int index, float delay)
        {
            base.Ini(index, delay);
            _text.text = toolType.ToString();
        }

        public override void Select()
        {
            base.Select();
            EventToolSelected context = new EventToolSelected(toolType);
            EventBusProvider.EventBus.InvokeEvent(context);
        }
    }
}
