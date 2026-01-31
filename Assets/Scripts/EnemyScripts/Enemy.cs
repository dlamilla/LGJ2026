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
    public Transform target;
    public EnemyType enemyType;

    public CapsuleCollider2D hurtbox;
    private Rigidbody2D rb;

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

        //rb = GetComponent<Rigidbody2D>();

        Agent = GetComponent<NavMeshAgent>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        EnemyStateFactory.Initialize(this, StateMachine);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StateMachine.Initialize(EnemyStateFactory.EnemyPatrolState);
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
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
        StartCoroutine(Cor());
    }

    IEnumerator Cor()
    {

        transform.position -= dir * 3;

        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(.05f);

        spriteRenderer.color = Color.white;

    }

    IEnumerator DoDamage()
    {

        Debug.Log("called");
        alreadyHitPlayer = true;

        EventBus.OnPlayerHit();

        yield return new WaitForSecondsRealtime(2);

        alreadyHitPlayer = false;
    }

    public bool IsPlayerInRange(float range)
    {
        //Vector2 a = transform.position;
        //Vector2 b = target.position;

        float sqrDist = (target.position -transform.position).sqrMagnitude;
        float sqrRange = range * range;

        return sqrDist <= sqrRange;
    }
}
