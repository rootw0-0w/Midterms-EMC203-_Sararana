using UnityEngine;

public class EnemyTypes : MonoBehaviour
{
    [SerializeField] SpriteRenderer enemyrenderer1;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject gemPrefab;
    [SerializeField] public float damage;
    [SerializeField] public int experienceToGive;
    [SerializeField] private float pushTime;

    private float pushCounter;
    private float speed = 3.5f;
    public float maxHealth;
    private float currentHealth;

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

             // push back
            if (pushCounter > 0){
                pushCounter -= Time.deltaTime;
                if (speed > 0){
                    speed = -speed;
                }
                if (pushCounter <= 0){
                    speed = Mathf.Abs(speed);
                }
            }
            

        // Move towards player
        transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
    }

public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        pushCounter = pushTime;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 0.5f);
        
        GameObject gems = Instantiate(gemPrefab, transform.position, Quaternion.identity);   
        
        Gem gemScript = gems.GetComponent<Gem>();
        if (gemScript != null)
        {
            gemScript.experienceToGive = experienceToGive; // Pass your enemy's XP value here
        };

        // Return to pool
        EnemyFactory.I.ReleaseEnemy1(gameObject);
        
    }
}
