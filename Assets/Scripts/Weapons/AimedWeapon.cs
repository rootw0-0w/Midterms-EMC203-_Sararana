using UnityEngine;

public class AimedWeapon : Weapon
{
    [SerializeField] private GameObject aimedprefab;
    private float spawnCounter;

    void Update()
    {
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0){
            spawnCounter = stats[weaponLevel].cooldown;
            for (int i = 0; i < stats[weaponLevel].amount; i++){
                Instantiate(aimedprefab, transform.position, transform.rotation, transform);
            }
        }
    }
}
