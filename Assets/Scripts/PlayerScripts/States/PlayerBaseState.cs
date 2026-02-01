using System.Collections;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerBaseState : State<Player>
{
    protected Animator animator;
    protected PlayerStateFactory playerStateFactory;
    protected Rigidbody2D rb;

    protected Vector3 dir;
    protected Vector3 dirToMouse;
    protected float xInput, yInput;
    protected float last_xInput, last_yInput;

    protected float x, y;

    protected bool canMove;

    public PlayerBaseState(Player entity, StateMachine<Player> stateMachine) : base(entity, stateMachine)
    {
        playerStateFactory = entity.PlayerStateFactory;
        rb = entity.rb;
        animator = entity.animator;
    }

    public override void Enter()
    {
        canMove = true;
    }

    public override void Exit()
    {
        
    }

    public override void FixedUpdate()
    {
        
    }

    public override void Update()
    {

        //Debug.Log($"im in {stateMachine.CurrentState}");

        if (canMove)
        {
            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");
        }


        dir = new Vector3(xInput, yInput).normalized;

        if(xInput != 0 || yInput != 0)
        {
            last_xInput = xInput;
            last_yInput = yInput;
        }


        //animator.SetFloat("last_moveX", last_xInput);
        //animator.SetFloat("last_moveY", last_yInput);


    }
}
