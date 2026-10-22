using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private float speed = 5f;
    [SerializeField] Animator animator;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
       PlayerMove(); 
    } 

    void PlayerMove() // Input functions for player movement
    {
    float horizontalInput = Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");

    transform.Translate(Vector2.up * Time.deltaTime * speed * verticalInput);
    transform.Translate(Vector2.right * Time.deltaTime * speed * horizontalInput);
    
    //ANIMATION
    animator.SetFloat("movex", horizontalInput);
    animator.SetFloat("movey", verticalInput);
      
      if (horizontalInput == 0 && verticalInput == 0)
        {
            animator.SetBool("moving", false);
        }
        else
        {
            animator.SetBool("moving", true);
        }

    //Player Restriction
    Vector3 pos = transform.position;
    pos.x = Mathf.Clamp(pos.x, -19.5f, 21.6f);
    pos.y = Mathf.Clamp(pos.y, -16.1f, 10.3f);
    transform.position = pos;
        
    }
}
