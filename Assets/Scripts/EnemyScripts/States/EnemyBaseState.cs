using Unity.Cinemachine;
using UnityEngine;

public class EnemyBaseState : State<Enemy>
{
    protected Animator animator;
    protected EnemyStateFactory enemyStateFactory;

    protected bool chaseCooldown;

    private float updateTime;
    public EnemyBaseState(Enemy entity, EnemyStateFactory enemyStateFactory, StateMachine<Enemy> stateMachine) : base(entity, enemyStateFactory, stateMachine)
    {
        animator = entity.animator;
        this.enemyStateFactory = entity.EnemyStateFactory;
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

        if (entity.hp <= 0)
        {
            stateMachine.ChangeState(enemyStateFactory.EnemyDeathState);
            return;
        }

        if (entity.IsPlayerInRange(50) && entity.enemyType == EnemyType.melee)
        {
            entity.isPlayerDeteced = true;
            stateMachine.ChangeState(enemyStateFactory.EnemyChaseState);
        }

        if(entity.IsPlayerInRange(60) && entity.enemyType == EnemyType.range)
        {
            entity.isPlayerDeteced = true;
        }

        if (entity.isPlayerDeteced)
        {
            if(entity.enemyType == EnemyType.range && !entity.chaseCooldown)
            {
                stateMachine.ChangeState(enemyStateFactory.EnemyChaseState);
            }

            if(entity.enemyType == EnemyType.melee)
            {
            stateMachine.ChangeState(enemyStateFactory.EnemyChaseState);

            }
        }
        
    }
}
