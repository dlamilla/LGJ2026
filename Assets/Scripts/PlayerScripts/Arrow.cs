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

}
