using System.Collections;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(Player entity, StateMachine<Player> stateMachine) : base(entity, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        entity.swordColl.enabled = true;
        entity.StartCoroutine(Cor());
    }

    public override void Exit()
    {
        base.Exit();

        entity.swordColl.enabled = false;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        Vector3 nextPos = entity.transform.position + dir * entity.walkSpeed * Time.deltaTime;

        rb.MovePosition(nextPos);
    }

    public override void Update()
    {
        base.Update();
    }

    IEnumerator Cor()
    {
        yield return new WaitForSecondsRealtime(.2f);
        stateMachine.ChangeState(playerStateFactory.IdleState);
    }
}
