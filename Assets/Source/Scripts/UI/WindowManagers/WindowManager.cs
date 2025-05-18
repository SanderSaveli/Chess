using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace OFG.ChessPeak
{
    [Serializable]
    public class WindowParams<T>
    {
        [HideInInspector] public string Name;
        public T WindowType;
        public UIScreen Screen;
    }
    public class PopupParams
    {
        public int Order;
        public UIScreen Screen;
    }
    public class WindowManager<WindowType> : MonoBehaviour
    {
        [Header(H.Components)]
        public List<WindowParams<WindowType>> Windows = new();

        protected SignalBus _signalBus;
        protected UIScreen _openedScreen;
        protected UIScreen _openedPopup;

        protected Queue<PopupParams> _popupQueue;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Start()
        {
            foreach (var window in Windows)
            {
                if (window.Screen.IsShowAtStart)
                {
                    _openedScreen = window.Screen;
                    break;

                }
            }
        }

        public void CloseOpenedWindow()
        {
            _openedScreen?.Hide();
        }

        protected void OpenNewVindow(WindowParams<WindowType> windowParams)
        {
            _openedScreen?.Hide();
            windowParams.Screen.Show();
            _openedScreen = windowParams.Screen;
        }

#if UNITY_EDITOR
        protected void OnValidate()
        {
            foreach (var window in Windows)
            {
                window.Name = window.WindowType.ToString();
            }
        }
#endif
        public void AddToPopupQueue(UIScreen screen, int order)
        {
            PopupParams popupParams = new PopupParams();
            popupParams.Screen = screen;
            popupParams.Order = order;
            _popupQueue.Enqueue(popupParams);
            _popupQueue.OrderBy(t => t.Order);
        }

        public void OpenNextPopup()
        {
            if (_openedPopup != null)
            {
                _openedPopup.Hide();
            }
            if (_popupQueue.TryDequeue(out PopupParams popup))
            {
                _openedPopup = popup.Screen;
                _openedPopup.Show();
            }
            else
            {
                _openedPopup = null;
            }
        }
    }
}
