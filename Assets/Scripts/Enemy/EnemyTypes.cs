using UnityEngine;

public class EnemyTypes : MonoBehaviour
{
    [SerializeField] SpriteRenderer enemyrenderer1;
     public float speed = 10f;

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = Player.Instance.transform.position;

         // face the player
            if (Player.Instance.transform.position.x > transform.position.x){
                enemyrenderer1.flipX = false;
            } else {
                enemyrenderer1.flipX = true;
            }

        // Move towards player
        transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
    }
}
