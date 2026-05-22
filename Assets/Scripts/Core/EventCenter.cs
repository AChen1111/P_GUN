using System;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace Game.Core
{
    public static class EventCenter
    {
        private static readonly Dictionary<GameEventId, List<Action>> eventMap = new Dictionary<GameEventId, List<Action>>();
        private static readonly Dictionary<GameEventId, object> payloadEventMap = new Dictionary<GameEventId, object>();

        public static void AddListener(GameEventId eventId, Action listener)
        {
            if (eventId == null) throw new ArgumentNullException(nameof(eventId));
            if (listener == null) return;

            var listeners = GetListeners(eventId);
            if (!listeners.Contains(listener))
            {
                listeners.Add(listener);
            }
        }

        public static void AddListener<TPayload>(GameEventId<TPayload> eventId, Action<TPayload> listener)
        {
            if (eventId == null) throw new ArgumentNullException(nameof(eventId));
            if (listener == null) return;

            var listeners = GetPayloadListeners(eventId);
            if (!listeners.Contains(listener))
            {
                listeners.Add(listener);
            }
        }

        public static void RemoveListener(GameEventId eventId, Action listener)
        {
            if (eventId == null) throw new ArgumentNullException(nameof(eventId));
            if (listener == null) return;
            if (!eventMap.TryGetValue(eventId, out var listeners)) return;

            listeners.Remove(listener);
            if (listeners.Count == 0)
            {
                eventMap.Remove(eventId);
            }
        }

        public static void RemoveListener<TPayload>(GameEventId<TPayload> eventId, Action<TPayload> listener)
        {
            if (eventId == null) throw new ArgumentNullException(nameof(eventId));
            if (listener == null) return;
            if (!payloadEventMap.TryGetValue(eventId, out var rawListeners)) return;

            var listeners = (List<Action<TPayload>>)rawListeners;
            listeners.Remove(listener);
            if (listeners.Count == 0)
            {
                payloadEventMap.Remove(eventId);
            }
        }

        public static void Trigger(GameEventId eventId)
        {
            if (eventId == null) throw new ArgumentNullException(nameof(eventId));
            if (!eventMap.TryGetValue(eventId, out var listeners)) return;

            // 触发时复制当前监听列表, 允许回调内安全增删监听.
            var invokeList = ListPool<Action>.Get();
            try
            {
                invokeList.AddRange(listeners);
                for (var i = 0; i < invokeList.Count; i++)
                {
                    invokeList[i]?.Invoke();
                }
            }
            finally
            {
                invokeList.Clear();
                ListPool<Action>.Release(invokeList);
            }
        }

        public static void Trigger<TPayload>(GameEventId<TPayload> eventId, TPayload payload)
        {
            if (eventId == null) throw new ArgumentNullException(nameof(eventId));
            if (!payloadEventMap.TryGetValue(eventId, out var rawListeners)) return;

            var listeners = (List<Action<TPayload>>)rawListeners;
            // 触发时复制当前监听列表, 允许回调内安全增删监听.
            var invokeList = ListPool<Action<TPayload>>.Get();
            try
            {
                invokeList.AddRange(listeners);
                for (var i = 0; i < invokeList.Count; i++)
                {
                    invokeList[i]?.Invoke(payload);
                }
            }
            finally
            {
                invokeList.Clear();
                ListPool<Action<TPayload>>.Release(invokeList);
            }
        }

        public static void Clear(GameEventId eventId)
        {
            if (eventId == null) throw new ArgumentNullException(nameof(eventId));

            eventMap.Remove(eventId);
            payloadEventMap.Remove(eventId);
        }

        public static void ClearAll()
        {
            eventMap.Clear();
            payloadEventMap.Clear();
        }

        private static List<Action> GetListeners(GameEventId eventId)
        {
            if (!eventMap.TryGetValue(eventId, out var listeners))
            {
                listeners = new List<Action>();
                eventMap[eventId] = listeners;
            }

            return listeners;
        }

        private static List<Action<TPayload>> GetPayloadListeners<TPayload>(GameEventId<TPayload> eventId)
        {
            if (!payloadEventMap.TryGetValue(eventId, out var rawListeners))
            {
                var newListeners = new List<Action<TPayload>>();
                payloadEventMap[eventId] = newListeners;
                return newListeners;
            }

            return (List<Action<TPayload>>)rawListeners;
        }
    }
}
