using UnityEngine;
using TMPro;

public class BalanceScore : MonoBehaviour
{
    public BikeBalance bike;
    public TextMeshProUGUI scoreText;

    private float score = 0f;

    void Update()
    {
        if (!bike.isFallen)
        {
            float angle = bike.currentAngle;
            if (angle >= 75f && angle <= 105f)
            {
                score += Time.deltaTime * 10f;
                scoreText.text = Mathf.FloorToInt(score).ToString();
            }
        }
    }
}