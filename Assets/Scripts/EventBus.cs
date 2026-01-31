using System;
using UnityEngine;

public static class EventBus
{
    public static event Action OnPlayerHitEvent;

    public static void OnPlayerHit()
    {
        OnPlayerHitEvent?.Invoke();
    }
}
