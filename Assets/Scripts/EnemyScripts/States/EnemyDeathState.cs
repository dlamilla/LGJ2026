using System.Collections;
using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    public EnemyDeathState(Enemy entity, EnemyStateFactory enemyStateFactory, StateMachine<Enemy> stateMachine) : base(entity, enemyStateFactory, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        EventBus.OnEnemyDeath(entity);

        entity.hurtbox.enabled = false;

        entity.Agent.ResetPath();
        entity.Agent.velocity = Vector3.zero;
        entity.Agent.isStopped = true;

        entity.StartCoroutine(CorDeath());
        animator.Play("Death");
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
    }

    IEnumerator CorDeath()
    {
        yield return new WaitForSecondsRealtime(.7f);
        Object.Destroy(entity.gameObject);
    }
}
