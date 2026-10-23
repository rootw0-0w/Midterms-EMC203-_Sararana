using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [Header("UI References")]
    public Image healthBar;


    [Header("Settings")]
    public float changespeed = 10f;

    public static float EaseOutCubic(float t) => 1 - (1 - t) * (1 - t) * (1 - t);

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
    }
}