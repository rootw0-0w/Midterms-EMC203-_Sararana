using UnityEngine;
using System.Collections.Generic;


 [System.Serializable] //wave objects
    public class Wave{
    public float spawnTimer;
    public float spawnInterval;
    public int enemiesPerWave;
    public int spawnedEnemyCount;

    //MINIBOSS
    public bool spawnMiniBoss;       
    public int spawnBossAfterEnemies;
    public GameObject miniBossPrefab; 
    public bool bossHasSpawned;
    }

public class GameController : MonoBehaviour
{
   public Transform[] spawnPoints;
    public static GameController I;

    
    public List<Wave> waves;
    public int waveNumber;

    void Awake()
    {
        I = this; 
    }

    //Uses a list 
    void Update()
    {
        if (Player.Instance.gameObject.activeSelf){
            waves[waveNumber].spawnTimer += Time.deltaTime;
            if (waves[waveNumber].spawnTimer >= waves[waveNumber].spawnInterval){
                waves[waveNumber].spawnTimer = 0;
                SpawnEnemy();
            }

            // Spawns the mini boss if the wave has one
            if (waves[waveNumber].spawnMiniBoss && !waves[waveNumber].bossHasSpawned && waves[waveNumber].spawnedEnemyCount >= waves[waveNumber].spawnBossAfterEnemies)
            {
                waves[waveNumber].bossHasSpawned = true;
                SpawnMiniBoss();
            }
            
            //Escalates the wave after one is finished
            if (waves[waveNumber].spawnedEnemyCount >= waves[waveNumber].enemiesPerWave){
                waves[waveNumber].spawnedEnemyCount = 0;
                if (waves[waveNumber].spawnInterval > 0.3f){
                    waves[waveNumber].spawnInterval *= 0.9f;
                }
                waveNumber++;
            }
            if (waveNumber >= waves.Count){ //Resets Wavecounter
                waveNumber = 0;
            }
        }
    }

     public void SpawnEnemy()
    {
        // picks a random spawn point
        int i = Random.Range(0, spawnPoints.Length);
        Transform p = spawnPoints[i];

        //uses pooling to spawn enemy
        EnemyFactory.I.SpawnEnemy1(p.position, p.rotation, Vector3.one);

        //Increases Enemy Count after wave is finished
        waves[waveNumber].spawnedEnemyCount++;

    }

     private void SpawnMiniBoss()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // MiniBoss Instantiated
        Instantiate(waves[waveNumber].miniBossPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}