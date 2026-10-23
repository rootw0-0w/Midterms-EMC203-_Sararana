using UnityEngine;
using System.Collections.Generic;

public class EnemyFactory : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    //pooling
    Queue<GameObject> enemyPool = new Queue<GameObject>();

    //poolsize
    [SerializeField] private int enemyPoolSize = 50;

    public static EnemyFactory I;
   
   void Awake()
    {
        I = this;
    }
    void Start()
    {
    if (enemyPrefabs.Length > 0){
        SetupPool(enemyPrefabs, enemyPoolSize, enemyPool);
    }else{
        Debug.LogError("No enemy prefabs assigned!"); }
    }
    

     void SetupPool(GameObject[] prefabs, int count, Queue<GameObject> pool)
    {
        for (int i = 0; i < count; i++)
        {
        //pick in random on the list of prefabs at each pool
        int randomIndex = Random.Range(0, prefabs.Length);
        GameObject chosenPrefab = prefabs[randomIndex];

        // every time instantiates the randomized prefab
        GameObject obj = Instantiate(chosenPrefab);
        obj.SetActive(false);
        pool.Enqueue(obj);
        }
    }

    GameObject GrabPool(Queue<GameObject> pool)
    {
        if (pool.Count > 0)
            return pool.Dequeue();
        return null;
    }

    public void SpawnEnemy1(Vector3 pos, Quaternion rot, Vector3 scale)
    {
        GameObject enemy = GrabPool(enemyPool);
        
        if(enemy == null)
        return;

        enemy.transform.SetPositionAndRotation(pos,rot);
        enemy.transform.localScale = scale;
        enemy.SetActive(true);

    }

    public void ReleaseEnemy1(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool.Enqueue(enemy);
    }


}
