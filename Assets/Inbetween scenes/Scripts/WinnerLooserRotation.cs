using UnityEngine;

public class WinnerLoserRotation : MonoBehaviour
{
    void Start()
    {
        if (GameData.instance.winner == 2)
        {
            // Top player vinder - roter 180 grader omkring Z
            Debug.Log("Player 2 vinder - roterer 180 grader");
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (GameData.instance.winner == 1)
        {
            // Bottom player vinder - ingen rotation
            Debug.Log("Player 1 vinder - ingen rotation");
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}