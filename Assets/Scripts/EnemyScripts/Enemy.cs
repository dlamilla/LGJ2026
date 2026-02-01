using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyType
{
    none,
    melee,
    range,
    boss
}

public class Enemy : MonoBehaviour
{
    public float hp;
    public float speed;
    public float distanceToAttack;
    public Transform target;
    public EnemyType enemyType;
    public bool isPlayerDeteced;
    public float knockBackForce;

    public bool chaseCooldown;
    public bool canAttackPlayer;
    public bool canChargeAttack;
    public bool canLookAtPlayer;
    public GameObject tntPrefab;

    public Transform[] patrolPoints;

    public CapsuleCollider2D hurtbox;
    public Rigidbody2D rb {  get; private set; }

    private bool alreadyHitPlayer;

    public NavMeshAgent Agent {  get; private set; }    

    private CapsuleCollider2D hitbox;
    [Header("Image")]
    public SpriteRenderer spriteRenderer;

    public Animator animator;
    public EnemyStateFactory EnemyStateFactory {  get; private set; }
    public StateMachine<Enemy> StateMachine { get; private set; }

    public Vector3 dir;
    private void Awake()
    {
        StateMachine = new StateMachine<Enemy>();
        EnemyStateFactory = new EnemyStateFactory();
        animator = GetComponent<Animator>();

        if(enemyType == EnemyType.boss)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        Agent = GetComponent<NavMeshAgent>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        EnemyStateFactory.Initialize(this, StateMachine);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(enemyType == EnemyType.boss)
        {
            StateMachine.Initialize(EnemyStateFactory.EnemyIdleState);

        }
        else
        {
            StateMachine.Initialize(EnemyStateFactory.EnemyPatrolState);
        }
        
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;

        if(enemyType != EnemyType.boss)
        {
            foreach (var c in patrolPoints)
            {
                c.SetParent(null);
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        dir = (target.position - transform.position).normalized;



        StateMachine.CurrentState.Update();
    }

    private void FixedUpdate()
    {
        //rb.MovePosition(transform.position + dir * speed * Time.deltaTime);
        StateMachine.CurrentState.FixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHurtBox"))
        {
            if(!alreadyHitPlayer)
            StartCoroutine(DoDamage());
        }
    }

    public void OnHit(float damage)
    {
        hp -= damage;
        StartCoroutine(DamageFeel());
    }

    IEnumerator DamageFeel()
    {

        transform.position -= dir * knockBackForce;

        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(.05f);

        spriteRenderer.color = Color.white;

    }

    IEnumerator DoDamage()
    {

        Debug.Log("player got hit");
        alreadyHitPlayer = true;

        EventBus.OnPlayerHit();

        yield return new WaitForSecondsRealtime(2);

        alreadyHitPlayer = false;
    }

    public bool IsPlayerInRange(float range)
    {
        float sqrDist = (target.position -transform.position).sqrMagnitude;
        float sqrRange = range * range;

        return sqrDist <= sqrRange;
    }

    public bool InRangeToAttack(float range)
    {
        Vector3 dirToTarget = target.position - transform.position;

        float sqrDistance = dirToTarget.sqrMagnitude;

        float sqrRange = range * range;

        return sqrDistance <= sqrRange;
    }

    public void ShootTnt()
    {
        GameObject go = Instantiate(tntPrefab, transform.position, Quaternion.identity);
        StartCoroutine(TNTParabolicMove(go, transform.position, target.position, 1));
    }

    IEnumerator TNTParabolicMove(GameObject go, Vector3 startPos, Vector3 targetPos, float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;

            // Movimiento horizontal correcto (entre puntos FIJOS)
            Vector3 currentPos = Vector3.Lerp(startPos, targetPos, t);

            // Parábola perfecta
            float height = 7f;
            float yOffset = height * 4f * t * (1 - t);
            currentPos.y += yOffset;

            go.transform.position = currentPos;

            time += Time.deltaTime;
            yield return null;
        }

        go.transform.position = targetPos;
    }
}
