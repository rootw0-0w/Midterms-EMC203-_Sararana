using UnityEngine;
using System.Collections;

public class RocketShooter : MonoBehaviour
{
    public GameObject rocketPrefab;
    public int rocketCount = 5;
    public float shootDelay = 3f;

    void OnEnable()
    {
        StartCoroutine(ShootRockets());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator ShootRockets()
    {
        yield return new WaitForSeconds(1f); // Wait before first shot

        while (true) 
        {
            float rocketSpacing = 360f / rocketCount;

            for (int i = 0; i < rocketCount; i++)
            {
                float targetAngle = (i * rocketSpacing) + 45f;
                Quaternion spawnRotation = Quaternion.Euler(0, 0, targetAngle);

                // Spawn the rocket
                GameObject rocket = Instantiate(rocketPrefab, transform.position, spawnRotation);
                
                // Destroy it after 5 seconds
                Destroy(rocket, 2f); 
            }

            yield return new WaitForSeconds(shootDelay);
        }
    }
}