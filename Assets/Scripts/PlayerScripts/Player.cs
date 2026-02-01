using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool isInJaguarPhase;
    public float jaguarAttackDamage;

    public bool isDead;

    private bool alreadyHit;
    public CapsuleCollider2D hurtbox;
    public BoxCollider2D swordColl;
    public CinemachineImpulseSource impulseSource;
    public Seeker seeker;

    public SpriteRenderer SpriteRenderer {  get; private set; }
    Vector3 dirToMouse;

    public Transform arrowOrigin;
    public Transform swordOrigin;
    public GameObject arrowPrefab;

    public AnimatorStateInfo info;

    [HideInInspector] public TransformationModes transformationMode;
    public Rigidbody2D rb { get; private set; }
    public Animator animator { get; private set; }
    public StateMachine<Player> StateMachine { get; private set; }

    public PlayerStateFactory PlayerStateFactory { get; private set; }

    bool b;
    private void Awake()
    {
        StateMachine = new StateMachine<Player>();
        PlayerStateFactory = new PlayerStateFactory();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        SpriteRenderer = GetComponent<SpriteRenderer>();



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

        info = animator.GetCurrentAnimatorStateInfo(0);

        //float angle = Mathf.Atan2(mouseWorldPos.y, mouseWorldPos.x) * Mathf.Rad2Deg;

        //swordOrigin.rotation = Quaternion.Euler(0,0,angle);

        if (Input.GetMouseButtonDown(0) && StateMachine.CurrentState != PlayerStateFactory.AttackState)
        {
            StateMachine.ChangeState(PlayerStateFactory.AttackState);
            //impulseSource.GenerateImpulse();
        }

        if (Input.GetKey(KeyCode.Q) )
        {
            StateMachine.ChangeState(PlayerStateFactory.MorphState);
        }

        if(seeker.isBossDead && !b)
        {
            b = true;
            StartCoroutine(Cor());
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

    IEnumerator Cor()
    {
        yield return new WaitForSeconds(1.3f);

        SceneManager.LoadScene(2);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + dirToMouse * 4f);
    }
}
