using UnityEngine;

public class EnemyBaseState : State<Enemy>
{
    protected Animator animator;
    protected EnemyStateFactory enemyStateFactory;
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
        if (entity.hp <= 0)
        {
            stateMachine.ChangeState(enemyStateFactory.EnemyDeathState);
            return;
        }

        if (entity.IsPlayerInRange(50) && entity.enemyType == EnemyType.melee)
        {
            stateMachine.ChangeState(enemyStateFactory.EnemyChaseState);
        }
    }
}
