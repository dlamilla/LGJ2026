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
    }

    IEnumerator Cor()
    {
        yield return new WaitForSecondsRealtime(2);

        chaseCooldown = false;
    }
}
