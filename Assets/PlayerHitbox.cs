using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    private Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponentInParent<Player>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyHurtBox"))
        {
            if(collision.transform.parent.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.OnHit(player.jaguarAttackDamage);
            }
        }
    }
}
