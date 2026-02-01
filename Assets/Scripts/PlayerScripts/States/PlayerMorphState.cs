using UnityEngine;

public class PlayerMorphState : PlayerBaseState
{
    public PlayerMorphState(Player entity, StateMachine<Player> stateMachine) : base(entity, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animator.Play("Mutating");
        entity.isInJaguarPhase = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        if(entity.info.normalizedTime > 1)
        {
            stateMachine.ChangeState(playerStateFactory.IdleState);
        }

    }
}
