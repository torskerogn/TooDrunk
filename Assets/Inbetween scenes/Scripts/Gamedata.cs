using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    // Tracks which player won each game
    public int winner = 0; // 1 = player 1, 2 = player 2

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // stays alive between all scenes
            Application.targetFrameRate = 60; // smooth gameplay
        }
        else
        {
            Destroy(gameObject);
        }
    }
}