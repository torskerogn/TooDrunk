using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public int winnerLoserScene = 3;

    void Start()
    {
        if (CoordinationFlow.Instance != null)
        {
            CoordinationFlow.Instance.onTimeUp.AddListener(OnGameOver);
        }
    }

    void OnGameOver()
    {
        int bottomScore = CoordinationFlow.Instance.BottomScore;
        int topScore = CoordinationFlow.Instance.TopScore;

        if (topScore > bottomScore)
        {
            GameData.instance.winner = 2; // Player 2 vinder
            SceneManager.LoadScene(winnerLoserScene + 1); // Player 2 scene
        }
        else if (bottomScore > topScore)
        {
            GameData.instance.winner = 1; // Player 1 vinder
            SceneManager.LoadScene(winnerLoserScene); // Player 1 scene
        }
        else
        {
            GameData.instance.winner = 1; // Tie
            SceneManager.LoadScene(winnerLoserScene);
        }
    }
}