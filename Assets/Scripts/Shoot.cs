using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float speed = 7f;
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime); 

         if (Player.Instance != null) 
        {
            float dist = Vector2.Distance(transform.position, Player.Instance.transform.position);
            if (dist < 1f)
            {
            Player.Instance.TakeDamage(5);
            Destroy(gameObject); 
            }
        }
    }
}
