using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    [SerializeField] private List<int> ScoreForLevel;
    
    public int currentLevel { get; private set; }
    public int scoreForCurrentLevel { get; private set; }

    private void Awake()
    {
        if(Instance == null || Instance == this)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }


    public void SetLevelData(int level)
    {
        currentLevel = level;
        scoreForCurrentLevel = ScoreForLevel[level - 1];
    }

}
