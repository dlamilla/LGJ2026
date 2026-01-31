using System;
using UnityEngine;

public static class EventBus
{
    public static event Action OnPlayerHitEvent;

    public static event Action<Enemy> OnEnemyDeathEvent;

    public static void OnPlayerHit()
    {
        OnPlayerHitEvent?.Invoke();
    }

    public static void OnEnemyDeath(Enemy enemy)
    {
        OnEnemyDeathEvent?.Invoke(enemy);
    }
}
