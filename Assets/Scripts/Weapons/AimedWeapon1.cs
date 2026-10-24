using UnityEngine;

public class AimedWeaponObject: MonoBehaviour
{
    private AimedWeapon weapon;
    public float speed = 5f;
    public float turnSpeed = 200f;
    private Transform target;

    void Start()
    {
        weapon = GameObject.Find("Aimed Weapon").GetComponent<AimedWeapon>();
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); 
        float closestDistance = Mathf.Infinity;
        
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = enemy.transform;
            }
        }

        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        Destroy(gameObject, 4f);
    }

    void Update()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        transform.Translate(Vector2.up * speed * Time.deltaTime);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemyObj in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemyObj.transform.position);
            
            if (distance <= weapon.stats[weapon.weaponLevel].range)
            {
                EnemyTypes enemyScript = enemyObj.GetComponent<EnemyTypes>();
                if (enemyScript != null)
                {
                    enemyScript.TakeDamage(weapon.stats[weapon.weaponLevel].damage);
                }
                
                Destroy(gameObject);
                return; 
            }
        }
    }
}