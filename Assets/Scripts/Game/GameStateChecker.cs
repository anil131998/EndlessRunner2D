using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChecker : MonoBehaviour
{
    public bool isPaused { get; private set;  }

    private void Awake()
    {
        isPaused = false;
    }

    private void GamePaused()
    {
        isPaused = true;
    }

    private void GameResumed()
    {
        isPaused = false;
    }

    private void OnEnable()
    {
        GamePlayManager.gamePaused += GamePaused;
        GamePlayManager.gameResumed += GameResumed;
    }


    private void OnDisable()
    {
        GamePlayManager.gamePaused -= GamePaused;
        GamePlayManager.gameResumed -= GameResumed;
    }
}
