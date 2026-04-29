using UnityEngine;
using TMPro;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 Instance;

    public int blueScore = 0;
    public int orangeScore = 0;
    public float timeRemaining = 60f;
    public TextMeshProUGUI blueScoreText;
    public TextMeshProUGUI orangeScoreText;
    public TextMeshProUGUI timerText;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timerText != null)
                timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
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