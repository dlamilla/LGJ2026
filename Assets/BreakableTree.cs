using System.Collections.Generic;
using UnityEngine;

public class BreakableTree : MonoBehaviour
{
    public int attacksToDestroy;
    public int attackIndex;

    PolygonCollider2D polygonCollider;

    private SpriteRenderer spriteRenderer;
    public List<Sprite> sprites;

    private void Start()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(attackIndex >= attacksToDestroy)
        {
            polygonCollider.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitBox"))
        {

            spriteRenderer.sprite = sprites[attackIndex];
            attackIndex++;
        }
    }
}
