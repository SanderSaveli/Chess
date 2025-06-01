using OFG.ChessPeak.LevelBuild;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class FigureScrollElement : ThemeAnimatedScrollElement
    {
        [SerializeField] private ToolTypes toolType;
        [SerializeField] private string _toolKey;

        public override void Ini(int index)
        {
            base.Ini(index);
            _tableKey.SetText(_toolKey);
        }

        public override void Ini(int index, float delay)
        {
            base.Ini(index, delay);
            _tableKey.SetText(_toolKey);
        }

        public override void Select()
        {
            base.Select();
            EventToolSelected context = new EventToolSelected(toolType);
            EventBusProvider.EventBus.InvokeEvent(context);
        }
    }
}
