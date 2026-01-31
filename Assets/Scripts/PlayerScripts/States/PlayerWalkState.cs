using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(Player entity, StateMachine<Player> stateMachine) : base(entity, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
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

        if(xInput == 0 && yInput == 0)
        {
            stateMachine.ChangeState(playerStateFactory.IdleState);
        }

        if((xInput != 0 || yInput != 0) && Input.GetKey(KeyCode.C))
        {
            stateMachine.ChangeState(playerStateFactory.RunState);
        }
    }
}
