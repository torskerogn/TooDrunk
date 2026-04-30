using System;
using UnityEngine;
using UnityEngine.Events;

public class CoordinationFlow : MonoBehaviour
{
    public static CoordinationFlow Instance { get; private set; }

    [Serializable] public class ScoreEvent : UnityEvent<int> {}

    public ScoreEvent onBottomScoreChanged;
    public ScoreEvent onTopScoreChanged;
    public ScoreEvent onTimerChanged;
    public UnityEvent onTimeUp;

    public float timeLimit = 60f;

    public int BottomScore { get; private set; }
    public int TopScore { get; private set; }

    private float timeRemaining;
    private bool timerRunning = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        StartTimer();
    }

    void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    void Update()
    {
        if (!timerRunning) return;

        timeRemaining -= Time.deltaTime;

        int secondsLeft = Mathf.CeilToInt(timeRemaining);
        onTimerChanged?.Invoke(Mathf.Max(secondsLeft, 0));

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            timerRunning = false;
            onTimeUp?.Invoke();
            ResetScores();
            StartTimer();
        }
    }

    void StartTimer()
    {
        timeRemaining = timeLimit;
        timerRunning = true;
    }

    public void AddScore(PlayerSide side)
    {
        if (side == PlayerSide.Bottom)
        {
            BottomScore++;
            onBottomScoreChanged?.Invoke(BottomScore);
        }
        else
        {
            TopScore++;
            onTopScoreChanged?.Invoke(TopScore);
        }
    }

    public void ResetScores()
    {
        BottomScore = 0;
        TopScore = 0;
        onBottomScoreChanged?.Invoke(0);
        onTopScoreChanged?.Invoke(0);
    }
}
