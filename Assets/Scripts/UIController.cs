using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [Header("UI References")]
    public Image healthBar;
    [SerializeField] private TMP_Text healthText;
    public Image expBar;
     [SerializeField] private TMP_Text expText;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public GameObject winPanel;
    public GameObject levelUpPanel;
     [SerializeField] private TMP_Text timerText;


    [Header("Settings")]
    public float changespeed = 10f;
    public LevelUpButton[] levelUpButtons;

    public static float EaseOutCubic(float t) => 1 - (1 - t) * (1 - t) * (1 - t);
    public static float EaseInQuad(float t) => t * t;

    void Awake()
    {
        if (Instance != null && Instance != this){
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    void Update()
    {
        if (Player.Instance == null) return;
        float targetFill = Mathf.InverseLerp(0, Player.Instance.playerMaxHealth, Player.Instance.playerHealth);
        
        float t = Mathf.Clamp01(changespeed * Time.deltaTime);
        float easedT = EaseOutCubic(t);
        
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, targetFill, easedT);
         healthText.text = Mathf.RoundToInt(Player.Instance.playerHealth) + " / " + Player.Instance.playerMaxHealth;
    }

    public void UpdateTimer(float timer){
        float min = Mathf.FloorToInt(timer / 60f);
        float sec = Mathf.FloorToInt(timer % 60f);

        timerText.text = min + ":" + sec.ToString("00");
    }

    public void UpdateExperienceBar()
    {
    if (Player.Instance == null) return;

    //xp to finish level
    float xpForNextLevel = Player.Instance.playerLevels[Player.Instance.currentLevel - 1];

    //startlevelxp
    float xpForCurrentLevel = 0;
    if (Player.Instance.currentLevel > 1)
    {
        xpForCurrentLevel = Player.Instance.playerLevels[Player.Instance.currentLevel - 2];
    }

    //forceupdate
    float targetFill = Mathf.InverseLerp(xpForCurrentLevel, xpForNextLevel, Player.Instance.experience);
    expBar.fillAmount = targetFill;

    expText.text = Mathf.RoundToInt(Player.Instance.experience) + " / " + Mathf.RoundToInt(xpForNextLevel);
    }

    public void LevelUpPanelOpen(){
        levelUpPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LevelUpPanelClose(){
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    
}