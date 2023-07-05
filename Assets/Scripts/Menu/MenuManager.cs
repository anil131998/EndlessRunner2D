using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject levelSelectPanel;
    [SerializeField] private GameObject settingsPanel;

    public void OpenLevelSelect()
    {
        levelSelectPanel.SetActive(true);
    }
    public void CloseLevelSelect()
    {
        levelSelectPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void LoadLevel(int score)
    {
        DataManager.Instance.SetLevelData(score);
        SceneManager.LoadScene("Game");
    }

}
