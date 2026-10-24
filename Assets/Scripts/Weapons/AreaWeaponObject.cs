using System.Collections.Generic;
using UnityEngine;

public class AreaWeaponObject : MonoBehaviour
{
    public AreaWeapon weapon;
    private Vector3 targetSize;
    private float timer;
    private float counter;

    void Start()
    {
        weapon = GameObject.Find("AreaWeapon").GetComponent<AreaWeapon>();
        targetSize = Vector3.one * weapon.stats[weapon.weaponLevel].range;
        transform.localScale = Vector3.zero;
        timer = weapon.stats[weapon.weaponLevel].duration;
        //AudioController.Instance.PlaySound(AudioController.Instance.areaWeaponSpawn);
    }

    void Update()
    {
        // grow bigger to hit enemy
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, Time.deltaTime * 6f);
        
        // shrink and then destroy
        timer -= Time.deltaTime;
        if (timer <= 0){
            targetSize = Vector3.zero;
            if (transform.localScale.x == 0f){
                Destroy(gameObject);
                //AudioController.Instance.PlaySound(AudioController.Instance.areaWeaponDespawn);
            }
        }
        
        // set damage
        counter -= Time.deltaTime;
        if (counter <= 0){
           counter = weapon.stats[weapon.weaponLevel].speed;
            
            // detect all objects with the tag enemy
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            
            
           float currentRadius = weapon.stats[weapon.weaponLevel].range; 

            foreach (GameObject enemyObj in enemies)
            {
                float distance = Vector2.Distance(transform.position, enemyObj.transform.position);
                
                if (distance <= currentRadius)
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
}
