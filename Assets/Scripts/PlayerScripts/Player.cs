using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;

    public BoxCollider2D swordColl;
    public CinemachineImpulseSource impulseSource;
    Vector3 dirToMouse;

    public Transform arrowOrigin;
    public Transform swordOrigin;
    public GameObject arrowPrefab;
    public Rigidbody2D rb { get; private set; }
    public StateMachine<Player> StateMachine { get; private set; }

    public PlayerStateFactory PlayerStateFactory { get; private set; }
    private void Awake()
    {
        StateMachine = new StateMachine<Player>();
        PlayerStateFactory = new PlayerStateFactory();

        rb = GetComponent<Rigidbody2D>();
        impulseSource = GetComponent<CinemachineImpulseSource>();



        PlayerStateFactory.Initialize(this, StateMachine);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StateMachine.Initialize(PlayerStateFactory.IdleState);
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.CurrentState.Update();

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
            
        dirToMouse = (mouseWorldPos - transform.position).normalized;

        float angle = Mathf.Atan2(mouseWorldPos.y, mouseWorldPos.x) * Mathf.Rad2Deg;

        swordOrigin.rotation = Quaternion.Euler(0,0,angle);

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(arrowPrefab, arrowOrigin.position, Quaternion.identity);
            //impulseSource.GenerateImpulse();
        }

    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdate();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + dirToMouse * 4f);
    }
}
