using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;


    Vector3 dirToMouse;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void OnEnable()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        dirToMouse = (mouseWorldPos - transform.position).normalized;
        StartCoroutine(Cor());
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = dirToMouse * speed;
    }

    IEnumerator Cor()
    {
        yield return new WaitForSecondsRealtime(1.5f);

        Destroy(gameObject);
    }

}
