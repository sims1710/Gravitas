using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.UI;
using TMPro;

public class MissionControl : MonoBehaviour
{
    public GameObject startMenuUI;
    public GameObject gameOverUI;
    public GameObject victoryUI;

    public AstronautHealth astronautHealth;
    public Transform playerStartPosition;
    public GameObject[] enemies;

    void Start()
    {
        startMenuUI.SetActive(true);
        gameOverUI.SetActive(false);
        victoryUI.SetActive(false);

        Time.timeScale = 0;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        startMenuUI.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true); 
        Time.timeScale = 0;
    }

    public void GameComplete()
    {
        Time.timeScale = 0;
        victoryUI.SetActive(true);
    }
}
