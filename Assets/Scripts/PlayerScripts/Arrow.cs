using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damage;
    public float speed;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private CinemachineImpulseSource impulseSource;

    Vector3 dirToMouse;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        impulseSource = GetComponent<CinemachineImpulseSource>();
        
    }

    private void OnEnable()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        dirToMouse = mouseWorldPos - transform.position;

        float angle = Mathf.Atan2(dirToMouse.y, dirToMouse.x) * Mathf.Rad2Deg - 90;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        StartCoroutine(Cor());
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = dirToMouse.normalized * speed;
    }

    IEnumerator Cor()
    {
        yield return new WaitForSecondsRealtime(1.5f);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("EnemyHurtBox"))
        {
            //if (collision.transform.parent.TryGetComponent<Enemy>(out var enemy))
            //{
            //    enemy.OnHit(damage);
            //}
            //Destroy(gameObject);


            transform.SetParent(collision.transform);
            StartCoroutine(Delay(collision));
        }

    }

    IEnumerator Delay(Collider2D collision)
    {
        float randomX = Random.value > .5f ? -.25f : .25f;
        float randomY = Random.value < .5f ? -.25f : .25f;

        Vector3 rnd = new Vector3(randomX, randomY, .1f);

        impulseSource.GenerateImpulseWithVelocity(rnd);

        speed = 0;

        if (collision.transform.parent.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.OnHit(damage);
        }
        boxCollider.enabled = false;

        yield return null;
        //yield return new WaitForSecondsRealtime(1f);

        //Destroy(gameObject);
    }

}
