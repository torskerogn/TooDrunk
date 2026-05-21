using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 Instance;

    public int blueScore = 0;
    public int orangeScore = 0;
    public float timeRemaining = 60f;
    public TextMeshProUGUI blueScoreText;
    public TextMeshProUGUI orangeScoreText;
    public TextMeshProUGUI timerText;
    public int winnerLoserScene = 11;
    private bool gameEnded = false; // ← NYT!

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (gameEnded) return; // ← NYT!
        
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timerText != null)
                timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
        }
        else
        {
            gameEnded = true; // ← NYT!
            HandleGameOver();
        }
    }

    void HandleGameOver()
    {
        Debug.Log("GAME OVER - Blue: " + blueScore + " Orange: " + orangeScore);
        
        if (orangeScore > blueScore)
        {
            GameData.instance.winner = 2;
            SceneManager.LoadScene(winnerLoserScene + 1);
        }
        else if (blueScore > orangeScore)
        {
            GameData.instance.winner = 1;
            SceneManager.LoadScene(winnerLoserScene);
        }
        else
        {
            GameData.instance.winner = 1;
            SceneManager.LoadScene(winnerLoserScene);
        }
    }

    public void AddScore(string team, int points)
    {
        if (team == "blue")
        {
            blueScore += points;
            if (blueScoreText != null)
                blueScoreText.text = blueScore.ToString();
        }
        else if (team == "orange")
        {
            orangeScore += points;
            if (orangeScoreText != null)
                orangeScoreText.text = orangeScore.ToString();
        }
    }
}