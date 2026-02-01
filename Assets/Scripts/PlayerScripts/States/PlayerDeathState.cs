using UnityEngine;

public class PlayerDeathState : PlayerBaseState
{
    public PlayerDeathState(Player entity, StateMachine<Player> stateMachine) : base(entity, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        entity.isDead = true;

        entity.SpriteRenderer.sortingOrder = -61;

        if (entity.isInJaguarPhase)
        {
            animator.Play("DeathMutant");
        }
        else
        {
            animator.Play("Death");
        }

        entity.hurtbox.enabled = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        if(entity.info.normalizedTime >= 1)
        {

        }
    }
}
