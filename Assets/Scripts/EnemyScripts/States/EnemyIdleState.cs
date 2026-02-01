using System.Collections;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(Enemy entity, EnemyStateFactory enemyStateFactory, StateMachine<Enemy> stateMachine) : base(entity, enemyStateFactory, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if(entity.enemyType == EnemyType.range)
        {
            entity.StartCoroutine(Cor());
            entity.Agent.ResetPath();
            entity.Agent.isStopped = true;
            entity.Agent.velocity = Vector3.zero;
        }

        animator.Play("Idle");
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
        if (entity.IsPlayerInRange(60) && !entity.chaseCooldown)
        {
            stateMachine.ChangeState(enemyStateFactory.EnemyChaseState);
        }
    }

    IEnumerator Cor()
    {
        yield return new WaitForSecondsRealtime(3);

        entity.chaseCooldown = false;
    }
}
