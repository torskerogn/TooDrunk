using UnityEngine;
using TMPro;

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
            Debug.Log("GAME OVER — P1: " + Mathf.FloorToInt(playerOne.score) + " P2: " + Mathf.FloorToInt(playerTwo.score));
        }

        p1ScoreText.text = Mathf.FloorToInt(playerOne.score).ToString();
        p2ScoreText.text = Mathf.FloorToInt(playerTwo.score).ToString();
    }
}