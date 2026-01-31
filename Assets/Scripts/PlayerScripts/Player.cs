using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;

    public Rigidbody2D rb;
    public StateMachine<Player> StateMachine { get; private set; }

    public PlayerStateFactory PlayerStateFactory { get; private set; }
    private void Awake()
    {
        StateMachine = new StateMachine<Player>();
        PlayerStateFactory = new PlayerStateFactory();

        rb = GetComponent<Rigidbody2D>();

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
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.FixedUpdate();
    }
}
