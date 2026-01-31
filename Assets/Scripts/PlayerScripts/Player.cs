using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public enum TransformationModes
{
    none,
    human,
    beast
}

public class Player : MonoBehaviour
{
    public float hp;
    public float walkSpeed;
    public float runSpeed;

    public BoxCollider2D swordColl;
    public CinemachineImpulseSource impulseSource;
    Vector3 dirToMouse;

    public Transform arrowOrigin;
    public Transform swordOrigin;
    public GameObject arrowPrefab;

    [HideInInspector] public TransformationModes transformationMode; 
    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }
    public StateMachine<Player> StateMachine { get; private set; }

    public PlayerStateFactory PlayerStateFactory { get; private set; }
    private void Awake()
    {
        StateMachine = new StateMachine<Player>();
        PlayerStateFactory = new PlayerStateFactory();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        impulseSource = GetComponent<CinemachineImpulseSource>();



        PlayerStateFactory.Initialize(this, StateMachine);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StateMachine.Initialize(PlayerStateFactory.IdleState);
    }

    private void OnEnable()
    {
        EventBus.OnPlayerHitEvent += OnHit;
    }

    private void OnDisable()
    {
        EventBus.OnPlayerHitEvent -= OnHit;
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.CurrentState.Update();



        //float angle = Mathf.Atan2(mouseWorldPos.y, mouseWorldPos.x) * Mathf.Rad2Deg;

        //swordOrigin.rotation = Quaternion.Euler(0,0,angle);

        if (Input.GetMouseButtonDown(0) && StateMachine.CurrentState != PlayerStateFactory.AttackState)
        {
            StateMachine.ChangeState(PlayerStateFactory.AttackState);
            //impulseSource.GenerateImpulse();
        }

    }

    private void OnHit()
    {
        hp -= 1;
    }

    public void Shoot()
    {
        Instantiate(arrowPrefab, arrowOrigin.position, arrowOrigin.rotation);
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
