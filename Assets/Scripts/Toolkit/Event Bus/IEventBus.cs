using System;

namespace IUP.Toolkit
{
    public interface IEventBus
    {
        public void RegisterCallback<TEvent>(EventCallback<TEvent> callback);

        public void RegisterCallback(Delegate callback);

        public void RegisterCallbacks(params Delegate[] callbacks);

        public bool UnregisterCallback<TEvent>(EventCallback<TEvent> callback);

        public bool UnregisterCallback(Delegate callback);

        public void UnregisterCallbacks(params Delegate[] callbacks);

        public bool IsCallbackRegistered<TEvent>(EventCallback<TEvent> callback);

        public void InvokeEvent<TEvent>(TEvent context);

        public void InvokeEvent<TEvent>() where TEvent : new();
    }
}
