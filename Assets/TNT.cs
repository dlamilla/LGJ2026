using System.Collections;
using UnityEngine;

public class TNT : MonoBehaviour
{
    public float explosionDamage;
    public float flamesDamage;
    public int flamesDamageTimes;

    bool alreadyHit;

    SpriteRenderer spriteRenderer;
    CircleCollider2D circleCollider;
    Animator animator;
    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(Cor());
        
    }

    IEnumerator Cor()
    {
        yield return new WaitForSeconds(1.1f);
        //spriteRenderer.enabled = false;
        circleCollider.enabled = true;
        animator.Play("Explotion");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHurtBox") && !alreadyHit)
        {
            if (collision.transform.parent.TryGetComponent<Player>(out var player))
            {
                EventBus.OnPlayerHit();
                StartCoroutine(FlamesCor());
            }
        }
    }

    IEnumerator FlamesCor()
    {
        

        while (flamesDamageTimes < 3)
        {
            alreadyHit = true;
            circleCollider.enabled = false;
            yield return new WaitForSeconds(.8f);

            circleCollider.enabled = true;
            alreadyHit = false;
            flamesDamageTimes++;

            yield return null;

        }

        Destroy(gameObject);
    }
}
