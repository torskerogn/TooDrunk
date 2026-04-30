using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    public TMP_Text bottomScoreText;
    public TMP_Text topScoreText;
    public TMP_Text timerText;

    void Start()
    {
        UpdateBottom(0);
        UpdateTop(0);
    }

    public void UpdateBottom(int score)
    {
        if (bottomScoreText != null)
            bottomScoreText.text = score.ToString();
    }

    public void UpdateTop(int score)
    {
        if (topScoreText != null)
            topScoreText.text = score.ToString();
    }

    public void UpdateTimer(int seconds)
    {
        if (timerText != null)
            timerText.text = seconds <= 10 ? seconds.ToString() : "";
    }
}
