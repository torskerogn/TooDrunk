using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public int winnerLoserScene = 3; // change this for each game
    private float gameTime = 60f; // 60 seconds
    private float timeRemaining;

    void Start()
    {
        timeRemaining = gameTime;
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;
        
        if (timeRemaining <= 0)
        {
            GameData.instance.winner = 2; // bottom player always wins
            SceneManager.LoadScene(winnerLoserScene);
        }
    }
}