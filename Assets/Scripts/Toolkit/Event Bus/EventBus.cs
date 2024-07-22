using System.Collections.Generic;
using System;

namespace IUP.Toolkit
{
    public class EventBus : IEventBus
    {
        public EventBus() { }

        public EventBus(int capacity) => _callbacks = new(capacity);

        private readonly Dictionary<Type, List<Delegate>> _callbacks = new();

        public void RegisterCallback<TEvent>(EventCallback<TEvent> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }
            Type eventType = typeof(EventCallback<TEvent>);
            if (_callbacks.TryGetValue(eventType, out List<Delegate> specificCallbacks))
            {
                specificCallbacks.Add(callback);
            }
            else
            {
                specificCallbacks = new List<Delegate>() { callback };
                _callbacks.Add(eventType, specificCallbacks);
            }
        }

        public void RegisterCallback(Delegate callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }
            Type callbackType = callback.GetType();
            if (CheckCallbackType(callbackType))
            {
                if (_callbacks.TryGetValue(callbackType, out List<Delegate> specificCallbacks))
                {
                    specificCallbacks.Add(callback);
                }
                else
                {
                    specificCallbacks = new List<Delegate>() { callback };
                    _callbacks.Add(callbackType, specificCallbacks);
                }
            }
            else
            {
                throw new ArgumentException(nameof(callback));
            }
        }

        public void RegisterCallbacks(params Delegate[] callbacks)
        {
            foreach (Delegate callback in callbacks)
            {
                RegisterCallback(callback);
            }
        }

        public bool UnregisterCallback<TEvent>(EventCallback<TEvent> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }
            Type eventType = typeof(EventCallback<TEvent>);
            if (_callbacks.TryGetValue(eventType, out List<Delegate> specificCallbacks))
            {
                bool result = specificCallbacks.Remove(callback);
                if (specificCallbacks.Count == 0)
                {
                    _ = _callbacks.Remove(eventType);
                }
                return result;
            }
            return false;
        }

        public bool UnregisterCallback(Delegate callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }
            Type eventType = callback.GetType();
            if (_callbacks.TryGetValue(eventType, out List<Delegate> specificCallbacks))
            {
                bool result = specificCallbacks.Remove(callback);
                if (specificCallbacks.Count == 0)
                {
                    _ = _callbacks.Remove(eventType);
                }
                return result;
            }
            return false;
        }

        public void UnregisterCallbacks(params Delegate[] callbacks)
        {
            foreach (Delegate callback in callbacks)
            {
                UnregisterCallback(callback);
            }
        }

        public bool IsCallbackRegistered<TEvent>(EventCallback<TEvent> callback)
        {
            Type eventType = typeof(EventCallback<TEvent>);
            if (_callbacks.TryGetValue(eventType, out List<Delegate> specificCallbacks))
            {
                return specificCallbacks.Contains(callback);
            }
            return false;
        }

        public void InvokeEvent<TEvent>(TEvent context)
        {
            Type eventType = typeof(EventCallback<TEvent>);
            if (_callbacks.TryGetValue(eventType, out List<Delegate> specificCallbacks))
            {
                foreach (Delegate specificCallback in specificCallbacks)
                {
                    EventCallback<TEvent> callback = specificCallback as EventCallback<TEvent>;
                    callback(context);
                }
            }
        }

        public void InvokeEvent<TEvent>() where TEvent : new()
        {
            TEvent context = new();
            InvokeEvent(context);
        }

        private bool CheckCallbackType(Type callbackType)
        {
            Type callbackGenericTypeDefinition = callbackType.GetGenericTypeDefinition();
            return callbackGenericTypeDefinition == typeof(EventCallback<>);
        }
    }
}
