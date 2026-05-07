using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public void PressStart()
    {
        SceneManager.LoadScene(1); // loads Wheel video scene
    }
}