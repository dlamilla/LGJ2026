using System.Collections;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{

    float updatedTime;
    float timeToExitState;

    float valueX, valueY;

    Vector2 dirToMouse2;
    public PlayerAttackState(Player entity, StateMachine<Player> stateMachine) : base(entity, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();



        updatedTime = Time.time;
        timeToExitState = .2f;
        canMove = false;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        dirToMouse2 = ((Vector2)mouseWorldPos - (Vector2)entity.transform.position).normalized;

        animator.Play("Attack_BlendTree");

        animator.SetFloat("mousePosX", dirToMouse2.x);
        animator.SetFloat("mousePosY", dirToMouse2.y);

        entity.swordColl.enabled = true;

        entity.Shoot();
    }

    public override void Exit()
    {
        base.Exit();
        last_xInput = valueX;
        last_yInput = valueY;
        entity.swordColl.enabled = false;
        canMove = true;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0))
        {
            entity.Shoot();
            updatedTime = Time.time;
        }

        if (Time.time >= updatedTime + timeToExitState)
        {
            stateMachine.ChangeState(playerStateFactory.IdleState);
        }

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        dirToMouse2 = ((Vector2)mouseWorldPos - (Vector2)entity.transform.position).normalized;

        animator.SetFloat("mousePosX", dirToMouse2.x);
        animator.SetFloat("mousePosY", dirToMouse2.y);
    }

}
