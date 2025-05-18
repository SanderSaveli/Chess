
using IUP.Toolkit;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace OFG.ChessPeak
{
    public class MainMenuWindowManager : WindowManager<MenuScreens>
    {
        bool _isSub;
        private IEventBus _busProvider;
        [SerializeField] private BackgroundScroller _backgroundScroller;
        [SerializeField] private List<MenuScreens> _rightPositionScreens;
        public void OnInputOpenWindow(SignalInputOpenWindow ctx)
        {
            foreach (var window in Windows)
            {
                if (window.WindowType == ctx.Screen)
                {
                    OpenNewVindow(window);
                    if(_rightPositionScreens.Contains(window.WindowType))
                    {
                        _backgroundScroller.ScrollRight();
                    }
                    else
                    {
                        _backgroundScroller.ScrollLeft();
                    }
                }
            }
        }
    }
}
