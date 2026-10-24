using UnityEngine;

public class SpinObject : MonoBehaviour
{
    public SpinWeapon weapon; 
    public float hitRadius = 1f; 
    private float damageTimer;
    private float duration;
    
    private float offsetAngle; 

    void Start()
    {
        if (weapon == null) return;
        
        duration = weapon.stats[weapon.weaponLevel].duration;
        damageTimer = 0f;
    }

    void Update()
    {
        if (weapon == null || Player.Instance == null) return;

        offsetAngle += 90 * Time.deltaTime * weapon.stats[weapon.weaponLevel].speed;

        float rad = offsetAngle * Mathf.Deg2Rad;
        float range = weapon.stats[weapon.weaponLevel].range;
        
        Vector2 circleOffset = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * range;

        transform.position = Player.Instance.transform.position + (Vector3)circleOffset;

        duration -= Time.deltaTime;
        if (duration <= 0){
            Destroy(gameObject);
        }

        damageTimer -= Time.deltaTime;
        if (damageTimer <= 0)
        {
            damageTimer = 0.5f; 

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemyObj in enemies)
            {
                float distance = Vector2.Distance(transform.position, enemyObj.transform.position);
                
                if (distance <= hitRadius)
                {
                    EnemyTypes enemyScript = enemyObj.GetComponent<EnemyTypes>();
                    if (enemyScript != null)
                    {
                        enemyScript.TakeDamage(weapon.stats[weapon.weaponLevel].damage);
                    }
                }
            }
        }
    }

    public void SetRotationOffset(float rotationOffset)
    {
        offsetAngle = rotationOffset;
    }
}