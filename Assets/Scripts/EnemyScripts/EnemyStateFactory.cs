using UnityEngine;

public class EnemyStateFactory 
{
    public EnemyAttackState EnemyAttackState { get; private set; }

    public EnemyChaseState EnemyChaseState { get; private set; }
    public EnemyPatrolState EnemyPatrolState { get; private set;}

    public EnemyDeathState EnemyDeathState { get; private set; }

    public EnemyIdleState EnemyIdleState { get; private set; }

    public void Initialize(Enemy enemy, StateMachine<Enemy> stateMachine)
    {
        EnemyAttackState = new EnemyAttackState(enemy, this, stateMachine);
        EnemyChaseState = new EnemyChaseState(enemy, this, stateMachine);
        EnemyPatrolState = new EnemyPatrolState(enemy, this, stateMachine);
        EnemyDeathState = new EnemyDeathState(enemy, this, stateMachine);
        EnemyIdleState = new EnemyIdleState(enemy, this, stateMachine);
    }

}
