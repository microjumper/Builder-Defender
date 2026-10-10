using System;
using UnityEngine;

public abstract class EventChannelData<T> : ScriptableObject
{
    public event Action<T> EventRaised;

    public void RaiseEvent(T value) => EventRaised?.Invoke(value);
}
