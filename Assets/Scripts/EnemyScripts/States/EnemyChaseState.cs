    using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(Enemy entity, EnemyStateFactory enemyStateFactory, StateMachine<Enemy> stateMachine) : base(entity, enemyStateFactory, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();


        if(entity.enemyType != EnemyType.boss)
        {
            animator.Play("Chase_BlendTree");
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if((entity.enemyType == EnemyType.range || entity.enemyType == EnemyType.boss) && entity.InRangeToAttack(entity.distanceToAttack))
        {
            stateMachine.ChangeState(enemyStateFactory.EnemyAttackState);
        }

        entity.Agent.SetDestination(entity.target.position);

        if(entity.enemyType != EnemyType.boss)
        {
            animator.SetFloat("moveX", entity.dir.x);
            animator.SetFloat("moveY", entity.dir.y);
        }
    }
}
