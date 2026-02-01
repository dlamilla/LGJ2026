using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    float internTimer;
    public EnemyAttackState(Enemy entity, EnemyStateFactory enemyStateFactory, StateMachine<Enemy> stateMachine) : base(entity, enemyStateFactory, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        internTimer = 1;
        if (entity.enemyType == EnemyType.range)
        {
            entity.chaseCooldown = true;
            entity.Agent.ResetPath();
            entity.Agent.velocity = Vector3.zero;
            entity.Agent.isStopped = true;

            entity.ShootTnt();
        }

        if (entity.enemyType == EnemyType.boss)
        {
            entity.Agent.ResetPath();
            entity.Agent.velocity = Vector3.zero;
            entity.Agent.isStopped = true;

            entity.canLookAtPlayer = false;

            animator.Play("Attack");
        }
    }

    public override void Exit()
    {
        base.Exit();

        entity.Agent.isStopped = false;
        entity.canLookAtPlayer = true;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (entity.enemyType == EnemyType.range) stateMachine.ChangeState(enemyStateFactory.EnemyIdleState);

        if(entity.enemyType == EnemyType.boss)
        {
            internTimer -= Time.deltaTime;

            if(internTimer < 0)
            {
                stateMachine.ChangeState(enemyStateFactory.EnemyIdleState);
            }
        }
    }
}
