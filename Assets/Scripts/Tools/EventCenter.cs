using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 事件枚举
/// </summary>
public enum GameEvent
{
    PlayerHPChanged,
    PlayerDied,
    GameWin,
    GameOver,
    MiniMapToggleRequested,
    MiniMapShown,
    MiniMapHidden,
    ItemTipShown,
    ItemTipHidden,
    ItemPicked,
    ItemSpawned,
    ItemRemoved,
    BulletClipChanged,
    BulletBagChanged,
    PlayerHeadMessageRequested,
    DoorOpened,
    DoorClosed,
    AllRoomsGenerated
}

public struct PlayerHeadMessageEvent
{
    public string Message;
    public float Duration;

    public PlayerHeadMessageEvent(string message, float duration)
    {
        Message = message;
        Duration = duration;
    }
}

public static class EventCenter
{
    private static readonly Dictionary<GameEvent, HashSet<Action>> eventMap =
        new Dictionary<GameEvent, HashSet<Action>>();

    private static readonly Dictionary<GameEvent, HashSet<Delegate>> eventMapWithPayload =
        new Dictionary<GameEvent, HashSet<Delegate>>();

    public static void AddListener(GameEvent eventType, Action listener)
    {
        if (listener == null) return;
        GetListeners(eventType).Add(listener);
    }

    public static void AddListener<T>(GameEvent eventType, Action<T> listener)
    {
        if (listener == null) return;
        GetPayloadListeners(eventType).Add(listener);
    }

    public static void RemoveListener(GameEvent eventType, Action listener)
    {
        if (listener == null) return;

        if (!eventMap.TryGetValue(eventType, out var listeners)) return;
        listeners.Remove(listener);

        if (listeners.Count == 0)
        {
            eventMap.Remove(eventType);
        }
    }

    public static void RemoveListener<T>(GameEvent eventType, Action<T> listener)
    {
        if (listener == null) return;

        if (!eventMapWithPayload.TryGetValue(eventType, out var listeners)) return;
        listeners.Remove(listener);

        if (listeners.Count == 0)
        {
            eventMapWithPayload.Remove(eventType);
        }
    }

    public static void Trigger(GameEvent eventType)
    {
        if (!eventMap.TryGetValue(eventType, out var listeners)) return;

        foreach (var listener in CopyListeners(listeners))
        {
            listener?.Invoke();
        }
    }

    public static void Trigger<T>(GameEvent eventType, T payload)
    {
        if (!eventMapWithPayload.TryGetValue(eventType, out var listeners)) return;

        foreach (var listener in CopyListeners(listeners))
        {
            if (listener is Action<T> typedListener)
            {
                typedListener.Invoke(payload);
                continue;
            }

            Debug.LogWarning($"{nameof(EventCenter)}: {eventType} has a listener with a different payload type.");
        }
    }

    public static void Clear(GameEvent eventType)
    {
        eventMap.Remove(eventType);
        eventMapWithPayload.Remove(eventType);
    }

    public static void ClearAll()
    {
        eventMap.Clear();
        eventMapWithPayload.Clear();
    }

    private static HashSet<Action> GetListeners(GameEvent eventType)
    {
        if (!eventMap.TryGetValue(eventType, out var listeners))
        {
            listeners = new HashSet<Action>();
            eventMap[eventType] = listeners;
        }

        return listeners;
    }

    private static HashSet<Delegate> GetPayloadListeners(GameEvent eventType)
    {
        if (!eventMapWithPayload.TryGetValue(eventType, out var listeners))
        {
            listeners = new HashSet<Delegate>();
            eventMapWithPayload[eventType] = listeners;
        }

        return listeners;
    }

    private static List<Action> CopyListeners(HashSet<Action> listeners)
    {
        return new List<Action>(listeners);
    }

    private static List<Delegate> CopyListeners(HashSet<Delegate> listeners)
    {
        return new List<Delegate>(listeners);
    }
}
