using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    
    [Header("Player")]
    private float speed = 6f;
    [SerializeField] Animator animator;
    public float playerMaxHealth = 100f;
    public float playerHealth;

    public int experience;
    public int currentLevel;
    public int maxLevel;
    
    [Header("Weapons")]
    [SerializeField] private List<Weapon> inactiveWeapons;
    public List<Weapon> activeWeapons;
    [SerializeField] private List<Weapon> upgradeableWeapons;
    public List<Weapon> maxLevelWeapons;
    
    public List<int>playerLevels;

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

    void Start()
    {

        foreach (Weapon w in inactiveWeapons) {
         if (w != null) w.gameObject.SetActive(false);
        }
        
        for (int i = playerLevels.Count; i < maxLevel; i++){
            playerLevels.Add(Mathf.CeilToInt(playerLevels[playerLevels.Count - 1] * 1.1f + 15));
        }

        playerHealth = playerMaxHealth;
        UIController.Instance.UpdateExperienceBar();
        AddWeapon(0);
    }

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
      
    
    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        if(playerHealth <= 0)
        {
            gameObject.SetActive(false);
            GameManager.Instance.Gameover();
        }
    }

      public void GetExperience(int experienceToGet){
        experience += experienceToGet;
        UIController.Instance.UpdateExperienceBar();
         if (experience >= playerLevels[currentLevel - 1]){
             LevelUp();
         }
    }

    public void LevelUp(){
        experience -= playerLevels[currentLevel - 1];
        currentLevel++;
        UIController.Instance.UpdateExperienceBar();

        upgradeableWeapons.Clear();

        if (activeWeapons.Count > 0){
            upgradeableWeapons.AddRange(activeWeapons);
        }
        if (inactiveWeapons.Count > 0){
            upgradeableWeapons.AddRange(inactiveWeapons);
        }
        for (int i = 0; i < UIController.Instance.levelUpButtons.Length; i++){
        if (i < upgradeableWeapons.Count){
            UIController.Instance.levelUpButtons[i].ActivateButton(upgradeableWeapons[i]);
            UIController.Instance.levelUpButtons[i].gameObject.SetActive(true);
            } else {
                UIController.Instance.levelUpButtons[i].gameObject.SetActive(false);
            }
        }

        UIController.Instance.LevelUpPanelOpen();
    }

    private void AddWeapon(int index){
        activeWeapons.Add(inactiveWeapons[index]);
        inactiveWeapons[index].gameObject.SetActive(true);
        inactiveWeapons.RemoveAt(index);
    }

    public void ActivateWeapon(Weapon weapon){
    weapon.gameObject.SetActive(true);
    activeWeapons.Add(weapon);
    inactiveWeapons.Remove(weapon);
    }
}
