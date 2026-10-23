using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shootInterval = 8f; 
    public float detectionRange = 20f;

    private float nextFireTime = 0f;

    void Update()
    {
       Vector3 playerPos = Player.Instance.transform.position;
        if (playerPos == null) return;

        //calculates distance between player and enemy
        float playerDistance = Vector2.Distance(transform.position, playerPos);

        // Player Detection
        if (playerDistance <= detectionRange)
        {
            if (Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + shootInterval;
                FireBullet(playerPos);
            }
        }
        else{
            // Reset Timer when out of range
            nextFireTime = 0f; 
        }
    }

    void FireBullet(Vector3 targetPos)
    {
        Vector2 direction = (targetPos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        Quaternion bulletRotation = Quaternion.Euler(0, 0, angle);
        GameObject bullet = Instantiate(bulletPrefab, transform.position, bulletRotation);
        
        Destroy(bullet, 3f); 
    }
}