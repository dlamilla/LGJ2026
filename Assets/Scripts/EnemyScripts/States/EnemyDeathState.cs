using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDeathState : EnemyBaseState
{
    public EnemyDeathState(Enemy entity, EnemyStateFactory enemyStateFactory, StateMachine<Enemy> stateMachine) : base(entity, enemyStateFactory, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter(); 

        if(entity.enemyType == EnemyType.boss)
        {
            entity.seeker.isBossDead = true;
        }
        entity.isDead = true;
        
        EventBus.OnEnemyDeath(entity);

        entity.hurtbox.enabled = false;

        entity.Agent.ResetPath();
        entity.Agent.velocity = Vector3.zero;
        entity.Agent.isStopped = true;

        if(entity.enemyType != EnemyType.boss)
        {
            entity.StartCoroutine(CorDeath());
            entity.hitbox.enabled = false;
        }
        animator.Play("Death");
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
        if(entity.enemyType == EnemyType.boss)
        {
            if (entity.animatorStateInfo.normalizedTime >= 1)
            {
                SceneManager.LoadScene("EscenaFinal");
                //Object.Destroy(entity.gameObject);
            }
        }
    }

    IEnumerator CorDeath()
    {
        yield return new WaitForSecondsRealtime(.7f);
        Object.Destroy(entity.gameObject);
    }
}
