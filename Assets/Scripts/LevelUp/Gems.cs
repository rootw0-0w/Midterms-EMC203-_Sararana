using UnityEngine;

public class Gem : MonoBehaviour
{
    public float moveSpeed = 5f;         
    public float pickupRadius = 4f;     
    public float collectRadius = .8f;   
    public int experienceToGive;

    private Transform playerTransform;

    void Start()
    {
        //check for player
        if (Player.Instance != null)
        {
            playerTransform = Player.Instance.transform;
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        // Calculate distance between gem and player
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        
        if (distanceToPlayer < pickupRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }

        if (distanceToPlayer < collectRadius)
        {
            Collect();
        }
    }

    void Collect()
    {
        // Give the player experience
        Player.Instance.GetExperience(experienceToGive);
        
        // Destroy the gem
        Destroy(gameObject);
    }
}