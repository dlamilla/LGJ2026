using UnityEngine;

public class PlayerStateFactory
{
    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkState WalkState { get; private set; }
    public PlayerRunState RunState { get; private set; }

    public PlayerAttackState AttackState { get; private set; }

    public void Initialize(Player player, StateMachine<Player> stateMachine)
    {
        IdleState = new PlayerIdleState(player, stateMachine);
        WalkState = new PlayerWalkState(player, stateMachine);
        RunState = new PlayerRunState(player, stateMachine);
        AttackState = new PlayerAttackState(player, stateMachine);
    }

}
