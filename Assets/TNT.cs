using System.Collections;
using UnityEngine;

public class TNT : MonoBehaviour
{
    public float explosionDamage;
    public float flamesDamage;
    public int flamesDamageTimes;
    public int maxFlamesDamageTimes;

    bool alreadyHit;

    SpriteRenderer spriteRenderer;
    CircleCollider2D circleCollider;
    Animator animator;

    Coroutine coroutine;
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
        StartCoroutine(FlamesCor());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHurtBox") && !alreadyHit)
        {
            EventBus.OnPlayerHit();
            
        }
    }

    IEnumerator FlamesCor()
    {
        circleCollider.radius -= 1;

        while (flamesDamageTimes <= maxFlamesDamageTimes)
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
