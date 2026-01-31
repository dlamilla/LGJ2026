using UnityEngine;

public class PlayerBaseState : State<Player>
{
    protected PlayerStateFactory playerStateFactory;
    protected Rigidbody2D rb;

    protected Vector3 dir;
    protected float xInput, yInput;
    public PlayerBaseState(Player entity, StateMachine<Player> stateMachine) : base(entity, stateMachine)
    {
        playerStateFactory = entity.PlayerStateFactory;
        rb = entity.rb;
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Update()
    {
        Debug.Log($"im in {stateMachine.CurrentState}");

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        dir = new Vector3(xInput, yInput).normalized;

        if (Input.GetMouseButtonDown(1) && stateMachine.CurrentState != playerStateFactory.AttackState)
        {
            stateMachine.ChangeState(playerStateFactory.AttackState);
        }
    }
}
