using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(Player entity, StateMachine<Player> stateMachine) : base(entity, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        canMove = true;

        animator.Play("Idle_BlendTree");
        animator.SetFloat("last_moveX", last_xInput);
        animator.SetFloat("last_moveY", last_yInput);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(xInput != 0 || yInput != 0)
        {
            stateMachine.ChangeState(playerStateFactory.WalkState);
        }
    }
}
