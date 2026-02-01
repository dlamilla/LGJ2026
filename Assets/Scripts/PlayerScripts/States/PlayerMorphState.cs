using UnityEngine;

public class PlayerMorphState : PlayerBaseState
{
    float timer;
    public PlayerMorphState(Player entity, StateMachine<Player> stateMachine) : base(entity, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        timer = 1;
        animator.Play("Mutating");
        entity.isInJaguarPhase = true;
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

        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            stateMachine.ChangeState(playerStateFactory.IdleState);
        }
    }
}
