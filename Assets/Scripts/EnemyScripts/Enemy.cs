using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float hp;
    public float speed;
    public Transform target;
    private Rigidbody2D rb;

    private bool alreadyHitPlayer;

    private NavMeshAgent agent;

    private CapsuleCollider2D hitbox;
    [Header("Image")]
    public SpriteRenderer spriteRenderer;

    Vector3 dir;
    private void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();

        agent = GetComponent<NavMeshAgent>();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        dir = (target.position - transform.position).normalized;

        agent.SetDestination(target.position);

        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        //rb.MovePosition(transform.position + dir * speed * Time.deltaTime);
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
}
