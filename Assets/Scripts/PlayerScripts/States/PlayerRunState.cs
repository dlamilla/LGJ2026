using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(Player entity, StateMachine<Player> stateMachine) : base(entity, stateMachine)
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

        Vector3 nextPos = entity.transform.position + dir * entity.runSpeed * Time.deltaTime;

        rb.MovePosition(nextPos);
    }

    public override void Update()
    {
        base.Update();

        if (xInput == 0 && yInput == 0)
        {
            stateMachine.ChangeState(playerStateFactory.IdleState);
        }

        if ((xInput != 0 || yInput != 0) && Input.GetKeyUp(KeyCode.C))
        {
            stateMachine.ChangeState(playerStateFactory.WalkState);
        }

        animator.SetFloat("moveX", xInput);
        animator.SetFloat("moveY", yInput);
    }
}
