using UnityEngine;

public class SpinWeapon : Weapon
{
    public GameObject prefab;
    private float spawnCounter;

    void Update()
    {
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0){
            spawnCounter = stats[weaponLevel].cooldown;

            for (int i = 0; i < stats[weaponLevel].amount; i++){
                // Notice there is NO 'transform' at the end here!
                GameObject spawnedWeapon = Instantiate(prefab, transform.position, transform.rotation);
                
                SpinObject obj = spawnedWeapon.GetComponent<SpinObject>();
                if (obj != null)
                {
                    obj.weapon = this;
                    float rotation = 360f / stats[weaponLevel].amount * i;
                    obj.SetRotationOffset(rotation);
                }
            }
        }
    }
}