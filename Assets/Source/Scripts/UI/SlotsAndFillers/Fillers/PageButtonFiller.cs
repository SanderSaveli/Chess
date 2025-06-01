using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OFG.ChessPeak
{
    public class PageButtonFiller : ItemFiller<PageSlot, PageButtonContext>
    {
        public List<PageSlot> Buttons => _slots;

        public override void FillItems(List<PageButtonContext> items, Action loader = null, bool clear = false)
        {
            base.FillItems(items, loader, clear);

            LayoutRebuilder.ForceRebuildLayoutImmediate(_contentPatent as RectTransform);
        }
    }
}
