using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(Enemy entity, EnemyStateFactory enemyStateFactory, StateMachine<Enemy> stateMachine) : base(entity, enemyStateFactory, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (entity.enemyType == EnemyType.range)
        {
            chaseCooldown = true;
            entity.Agent.ResetPath();
            entity.Agent.velocity = Vector3.zero;
            entity.Agent.isStopped = true;
        }
    }

    public override void Exit()
    {
        base.Exit();

        entity.Agent.isStopped = false;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
    }
}
