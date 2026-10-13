using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private enum State
    {
        Ready,
        CountdownToStart,
        Playing,
        GameOver
    }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    public static GameManager Instance { get; private set; }

    private State state;
    private float waitingToStartTimer = 0f;
    private float countdownToStartTimeMax = 3f;
    private float countdownToStartTimer;
    [SerializeField] private float gamePlayingTimerMax = 25f;
    private float gamePlayingTimer;
    private bool isGamePaused = false;

    private void Awake()
    {
        Instance = this;
        state = State.Ready;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        StartNewGame();
    }

    private void Update()
    {
        if (isGamePaused) return;
        switch (state)
        {
            case State.Ready:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer <= 0f)
                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer <= 0f)
                {
                    state = State.Playing;
                    gamePlayingTimer = gamePlayingTimerMax;
                    countdownToStartTimer = 3f;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.Playing:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer <= 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                Time.timeScale = 0f;
                isGamePaused = true;
                break;
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }
    
    public float GetCountdownToStartTimer()
    {
        return countdownToStartTimer;
    }

    public bool IsCountdownToStart()
    {
        return state == State.CountdownToStart;
    }

    public bool IsGamePlaying()
    {
        return state == State.Playing;
    }

    public bool IsGamePaused()
    {
        return isGamePaused;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetPlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused == true)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
            
    }

    public void StartNewGame()
    {
        Time.timeScale = 1f;
        state = State.Ready;
        countdownToStartTimer = countdownToStartTimeMax;
        gamePlayingTimer = 0;
        isGamePaused = false;
    }
}

