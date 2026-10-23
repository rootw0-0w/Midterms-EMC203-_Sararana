using UnityEngine;

public class EnemyTypes : MonoBehaviour
{
    [SerializeField] SpriteRenderer enemyrenderer1;
    [SerializeField] GameObject explosionPrefab;
    private float speed = 3.5f;

    public int maxHealth = 20;
    private int currentHealth;

    void OnEnable()
    {
        currentHealth = maxHealth; 
    }

    void Update()
    {
        if (GameController.I == null || !Player.Instance.gameObject.activeSelf) 
        return;

        Vector3 playerPos = Player.Instance.transform.position;

         // face the player
            if (Player.Instance.transform.position.x > transform.position.x){
                enemyrenderer1.flipX = false;
            } else {
                enemyrenderer1.flipX = true;
            }

        // Move towards player
        transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);

        //Destroys when close
        if ((playerPos - transform.position).sqrMagnitude < 0.50f)   
        {
            Death();

        }
    }
public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        // Damage the player when the enemy dies
        if (Player.Instance != null) 
        {
            Player.Instance.TakeDamage(5);
        }

        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 0.5f);   
        
        // Return to pool
        EnemyFactory.I.ReleaseEnemy1(gameObject);
    }
}
