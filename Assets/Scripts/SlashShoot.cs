using UnityEngine;

public class SlashShoot : MonoBehaviour
{
    public float speed = 10f;
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime); 

         if (Player.Instance != null) 
        {
            float dist = Vector2.Distance(transform.position, Player.Instance.transform.position);
            if (dist < 0.5f)
            {
            Player.Instance.TakeDamage(5);
            Destroy(gameObject); 
            }
        }
    }
}
