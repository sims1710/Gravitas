using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score = 0;

    private int enemyKillScore = 10;
    private int bossKillScore = 100;
    private int flowerCollectScore = 5;

    private int maxLives = 6;

    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void OnEnemyKilled()
    {
        AddScore(enemyKillScore);
    }

    public void OnBossKilled()
    {
        AddScore(bossKillScore);
    }

    public void OnFlowerCollected()
    {
        AddScore(flowerCollectScore);
    }
}
