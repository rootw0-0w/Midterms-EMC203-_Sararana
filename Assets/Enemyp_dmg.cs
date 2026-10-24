using UnityEngine;

public class Enemyp_dmg : MonoBehaviour
{

    void Update()
    {
         if (Player.Instance != null) 
        {
            float dist = Vector2.Distance(transform.position, Player.Instance.transform.position);
            if (dist < 1f)
            {
            Player.Instance.TakeDamage(10);
            }
        }
    }
}