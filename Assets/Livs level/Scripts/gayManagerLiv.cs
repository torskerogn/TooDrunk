using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gayManagerLiv : MonoBehaviour
{
    [Header("Players")]
    public BikeBalance playerOne;
    public BikeBalanceP2 playerTwo;

    [Header("Score UI")]
    public TMP_Text p1ScoreText;
    public TMP_Text p2ScoreText;

    public float gameDuration = 60f;
    public float timeRemaining;
    public int winnerLoserScene = 7; // Sæt til den rigtige scene nummer!
    private bool gameOver = false;

    void Start()
    {
        timeRemaining = gameDuration;
    }

    void Update()
    {
        if (gameOver) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            gameOver = true;
            
            float p1Score = Mathf.FloorToInt(playerOne.score);
            float p2Score = Mathf.FloorToInt(playerTwo.score);
            
            Debug.Log("GAME OVER — P1: " + p1Score + " P2: " + p2Score);
            
            // Bestem vinder og transition
            if (p2Score > p1Score)
            {
                GameData.instance.winner = 2; // Player 2 vinder
                SceneManager.LoadScene(winnerLoserScene + 1); // Player 2 scene
            }
            else if (p1Score > p2Score)
            {
                GameData.instance.winner = 1; // Player 1 vinder
                SceneManager.LoadScene(winnerLoserScene); // Player 1 scene
            }
            else
            {
                GameData.instance.winner = 1; // Tie
                SceneManager.LoadScene(winnerLoserScene);
            }
            return;
        }

        p1ScoreText.text = Mathf.FloorToInt(playerOne.score).ToString();
        p2ScoreText.text = Mathf.FloorToInt(playerTwo.score).ToString();
    }
}