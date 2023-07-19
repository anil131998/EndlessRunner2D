using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GamePlayManager : MonoBehaviour
{
    public static event UnityAction gamePaused;
    public static event UnityAction gameResumed;

    [SerializeField] private TMP_Text score_text;
    [SerializeField] private TMP_Text coin_text;
    [SerializeField] private Image health_bar;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject VictoryPanel;
    [SerializeField] private GameObject DefeatPanel;

    [SerializeField] private int totalHealth = 4;
    private float score = 0;
    private int coin = 0;
    private int currentHealth; 
    private int scoreToBeat = 0;
    private bool isPaused = false;

    private void Awake()
    {
        currentHealth = totalHealth;
        score = 0;
        coin = 0;
        scoreToBeat = DataManager.Instance.scoreForCurrentLevel;
        isPaused = false;

        CloseDefeatPanel();
        CloseVictoryPanel();
        ClosePauseMenu();
    }

    private void Update()
    {
        if (!isPaused)
        {
            score += Time.deltaTime;
            score_text.text = "Distance : " + (int)score + " / " + scoreToBeat;

            if ((int)score >= scoreToBeat)
            {
                LevelWon();
            }
        }
    }

    private void coinCollected()
    {
        coin++;
        coin_text.text = "Coin : " + coin + " $";
    }

    private void PlayerHit()
    {
        currentHealth--;
        health_bar.fillAmount = (float)currentHealth / (float)totalHealth;

        if(currentHealth <= 0)
        {
            LevelLost();
        }
    }

    private void LevelWon()
    {
        PauseGame();
        OpenVictoryPanel();
    }

    private void LevelLost()
    {
        PauseGame();
        OpenDefeatPanel();
    }

    private void OnEnable()
    {
        Coin.playerCollectedCoin += coinCollected;
        Obstacle.playerHitObstacle += PlayerHit;
    }
    private void OnDisable()
    {
        Coin.playerCollectedCoin -= coinCollected;
        Obstacle.playerHitObstacle -= PlayerHit;
    }


    //Menu Panel controls

    public void OpenPauseMenu() => pausePanel.SetActive(true);
    public void ClosePauseMenu() => pausePanel.SetActive(false);
    public void OpenVictoryPanel() => VictoryPanel.SetActive(true);
    public void CloseVictoryPanel() => VictoryPanel.SetActive(false);
    public void OpenDefeatPanel() => DefeatPanel.SetActive(true);
    public void CloseDefeatPanel() => DefeatPanel.SetActive(false);

    
    private void PauseGame()
    {
        isPaused = true;
        gamePaused?.Invoke();
    }

    private void ResumeGame()
    {
        isPaused = false;
        gameResumed?.Invoke();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene("Game");
    }

}
