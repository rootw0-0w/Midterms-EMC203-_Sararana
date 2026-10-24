using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float gameTime;
    public bool gameActive;
    public float winTime = 480f; 

    void Awake(){
        if (Instance != null && Instance != this){
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    void Start(){
        gameActive = true;
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            Pause();
        }
    
        if (gameActive){
            gameTime += Time.deltaTime;
           UIController.Instance.UpdateTimer(gameTime);
           if (gameTime >= winTime) 
            {
                WinGame();
            }
        }
        
    }


    public void WinGame(){ 
        gameActive = false;
        Time.timeScale = 0f;
        UIController.Instance.winPanel.SetActive(true);
    }

    public void Gameover(){
        gameActive = false;
        StartCoroutine(ShowGameOverScreen());
    }

    IEnumerator ShowGameOverScreen(){
        yield return new WaitForSecondsRealtime(1.5f); 
        UIController.Instance.gameOverPanel.SetActive(true);
        //AudioController.Instance.PlaySound(AudioController.Instance.gameOver);
    }

    public void Restart(){
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Gameplay");
    }
    
     

    public void Pause(){
        if (UIController.Instance.levelUpPanel.activeSelf == false){
            
            if (UIController.Instance.pausePanel.activeSelf == true){
                Resume(); 
            } 
            else if (UIController.Instance.gameOverPanel.activeSelf == false){
                UIController.Instance.pausePanel.SetActive(true);
                Time.timeScale = 0f;
                //AudioController.Instance.PlaySound(AudioController.Instance.pause);
            }
        }
    }

    public void Resume(){
        UIController.Instance.pausePanel.SetActive(false);
        Time.timeScale = 1f;
        //AudioController.Instance.PlaySound(AudioController.Instance.unpause);
    }
     public void NewGame(){
        SceneManager.LoadScene("Game");
    }

    public void QuitGame(){
        Application.Quit();
    }
}