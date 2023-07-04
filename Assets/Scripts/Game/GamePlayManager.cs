using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{
    
    [SerializeField] private TMP_Text score_text;
    [SerializeField] private TMP_Text coin_text;
    [SerializeField] private Image health_bar;

    private int score = 0;
    private int coin = 0;
    private int currentHealth; 
    private int totalHealth = 4;
    private int scoreToBeat = 0;

    private void Start()
    {
        currentHealth = totalHealth;

        //if(MainManager.Instance != null)
            scoreToBeat = MainManager.Instance.scoreForCurrentLevel;
    }

    private void Update()
    {
        score = (int)Time.time;
        score_text.text = "Distance : " + score + " / " + scoreToBeat;

        if(score >= scoreToBeat)
        {
            //wonLevel
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
            //LevelLost
        }
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

}
