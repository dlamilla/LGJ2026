using System.Collections;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    int patrolIndex;
    Coroutine patrolCoroutine;

    public EnemyPatrolState(
        Enemy entity,
        EnemyStateFactory enemyStateFactory,
        StateMachine<Enemy> stateMachine
    ) : base(entity, enemyStateFactory, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        patrolIndex = 0;

        patrolCoroutine = entity.StartCoroutine(PatrolCor());
    }

    public override void Exit()
    {
        base.Exit();

        if (patrolCoroutine != null)
        {
            entity.StopCoroutine(patrolCoroutine);
            patrolCoroutine = null;
        }
    }

    public override void Update()
    {
        if (entity.IsPlayerInRange(50))
        {
            entity.isPlayerDeteced = true;
            stateMachine.ChangeState(enemyStateFactory.EnemyChaseState);
        }
    }

    IEnumerator PatrolCor()
    {
        animator.Play("Chase_BlendTree");

        while (true)
        {
            // Reinicio seguro del índice
            if (patrolIndex >= entity.patrolPoints.Length)
            {
                patrolIndex = 0;
            }

            Transform targetPoint = entity.patrolPoints[patrolIndex];

            entity.Agent.isStopped = false;
            entity.Agent.SetDestination(targetPoint.position);

            if(patrolIndex == 0)
            {
                animator.SetFloat("moveX", 0);
                animator.SetFloat("moveY", -1);
            }
            else
            {
                animator.SetFloat("moveX", 0);
                animator.SetFloat("moveY", 1);
            }


            // Espera hasta llegar
            yield return new WaitUntil(HasReachedDestination);

            // Detener agente
            entity.Agent.ResetPath();
            entity.Agent.isStopped = true;
            entity.Agent.velocity = Vector3.zero;

            animator.Play("Idle");

            // Pausa en el punto
            yield return new WaitForSeconds(2f);

            patrolIndex++;

            animator.Play("Chase_BlendTree");
        }
    }

    bool HasReachedDestination()
    {
        if (entity.Agent.pathPending)
            return false;

        if (entity.Agent.remainingDistance <= entity.Agent.stoppingDistance + 0.1f)
        {
            if (!entity.Agent.hasPath || entity.Agent.velocity.sqrMagnitude < 0.01f)
                return true;
        }

        return false;
    }
}
