using System;
using System.Collections.Generic;

namespace IUP.Toolkit
{
    public sealed class EventRegistrar
    {
        public EventRegistrar(IEventBus eventBus) =>
            EventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));

        public EventRegistrar(IEventBus eventBus, int capacity)
        {
            EventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _callbacks = new List<Delegate>(capacity);
        }

        public IEventBus EventBus { get; }

        private readonly List<Delegate> _callbacks = new();

        public EventRegistrar RegisterCallbacks(params Delegate[] callbacks)
        {
            EventBus.RegisterCallbacks(callbacks);
            _callbacks.AddRange(callbacks);
            return this;
        }

        public EventRegistrar RegisterCallback<TEvent>(EventCallback<TEvent> callback)
        {
            EventBus.RegisterCallback(callback);
            _callbacks.Add(callback);
            return this;
        }

        public EventRegistrar UnregisterCallback<TEvent>(EventCallback<TEvent> callback)
        {
            _ = EventBus.UnregisterCallback(callback);
            _ = _callbacks.Remove(callback);
            return this;
        }

        public EventRegistrar UnregisterAll()
        {
            foreach (Delegate callback in _callbacks)
            {
                _ = EventBus.UnregisterCallback(callback);
            }
            _callbacks.Clear();
            return this;
        }
    }
}
