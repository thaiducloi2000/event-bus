using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EventBus
{
    [CreateAssetMenu(fileName = "GameplayEvent", menuName = "EventBus/Gameplay")]
    public class GameplayEvent : EventBus
    {
        private readonly Dictionary<int, Delegate> listeners = new();

        public override void AddListener<T>(int eventID, EventCallback<T> callback)
        {
            if (listeners.ContainsKey(eventID))
            {
                listeners[eventID] = Delegate.Combine(listeners[eventID], callback);
            }
            else
            {
                listeners.Add(eventID, callback);
            }
        }

        public override void ClearAllListener()
        {
            listeners.Clear();
        }

        public override void PostEvent<T>(int eventID, T param = default)
        {
            if (listeners.TryGetValue(eventID, out Delegate existingCallback))
            {
                (existingCallback as EventCallback<T>)?.Invoke(param);
            }
        }

        public override void RemoveAllListener(int eventID)
        {
            if (listeners.ContainsKey(eventID))
                listeners[eventID] = null;
        }

        public override void RemoveListener<T>(int eventID, EventCallback<T> callback)
        {
            if (listeners.ContainsKey(eventID))
            {
                listeners[eventID] = Delegate.Remove(listeners[eventID], callback);
                if (listeners[eventID] == null)
                {
                    listeners.Remove(eventID);
                }
            }
        }
    }
}
