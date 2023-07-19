using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameStateChecker))]
public class PlatformMovement : MonoBehaviour
{
    private GameStateChecker gC;

    private void Awake()
    {
        gC = GetComponent<GameStateChecker>();
    }

    void Update()
    {
        if(!gC.isPaused)
            transform.Translate(Vector2.left * DataManager.Instance.levelMoveSpeed * Time.deltaTime);    
    }

}
