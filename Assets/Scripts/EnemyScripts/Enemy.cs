using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Transform target;
    private Rigidbody2D rb;

    private NavMeshAgent agent;

    public float timeFollow;
    float originalTimer;

    Vector3 dir;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        originalTimer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        dir = (target.position - transform.position).normalized;

        if (Time.time >= originalTimer + timeFollow)
        {
            agent.SetDestination(target.position);
        }
    }

    private void FixedUpdate()
    {
        //rb.MovePosition(transform.position + dir * speed * Time.deltaTime);
    }
}
