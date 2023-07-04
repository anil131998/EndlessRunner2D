using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    private void Awake()
    {
        if (Instance != null || Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    [SerializeField] private List<int> ScoreForLevel;
    
    public int currentLevel { get; private set; }
    public int scoreForCurrentLevel { get; private set; }

    public void SetLevelData(int level)
    {
        currentLevel = level;
        scoreForCurrentLevel = ScoreForLevel[level - 1];
    }

}
