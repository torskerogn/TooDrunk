using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int blueScore = 0;
    public float timeRemaining = 60f;
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
    }

    public void AddScore(int points)
    {
        blueScore += points;
        scoreText.text = blueScore.ToString();
    }
}