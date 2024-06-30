using System.Collections.Generic;
using System;

namespace IUP.Toolkit
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, List<object>> _callbacks = new();

        public static void RegisterCallback<TEvent>(EventCallback<TEvent> callback)
        {
            Type eventType = typeof(TEvent);
            if (_callbacks.TryGetValue(eventType, out List<object> specificCallbacks))
            {
                specificCallbacks.Add(callback);
            }
            else
            {
                specificCallbacks = new List<object>() { callback };
                _callbacks.Add(eventType, specificCallbacks);
            }
        }

        public static bool UnregisterCallback<TEvent>(EventCallback<TEvent> callback)
        {
            Type eventType = typeof(TEvent);
            if (_callbacks.TryGetValue(eventType, out List<object> specificCallbacks))
            {
                bool result = specificCallbacks.Remove(callback);
                if (specificCallbacks.Count == 0)
                {
                    _callbacks.Remove(eventType);
                }
                return result;
            }
            return false;
        }

        public static void InvokeEvent<TEvent>(TEvent context)
        {
            Type eventType = typeof(TEvent);
            if (_callbacks.TryGetValue(eventType, out List<object> specificCallbacks))
            {
                foreach (object callback in specificCallbacks)
                {
                    (callback as EventCallback<TEvent>)(context);
                }
            }
        }

        public static void InvokeEvent<TEvent>() where TEvent : new()
        {
            TEvent context = new();
            Type eventType = typeof(TEvent);
            if (_callbacks.TryGetValue(eventType, out List<object> specificCallbacks))
            {
                foreach (object callback in specificCallbacks)
                {
                    (callback as EventCallback<TEvent>)(context);
                }
            }
        }
    }
}
