using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    public event EventHandler OnStateChanged;

    private enum State { 
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }

    private State state;
    private float waitingToStartTimer = 1f;
    private float countDownTimer = 3;
    private float playTimer;
    private float playTimerMax = 20f;

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Update()
    {
        switch(state) {
            case State.WaitingToStart: 
                {
                    waitingToStartTimer -= Time.deltaTime;
                    
                    if (waitingToStartTimer < 0f) {
                        state = State.CountDownToStart;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                    break;
                }
            case State.CountDownToStart: 
                {
                    countDownTimer -= Time.deltaTime;

                    if (countDownTimer < 0f) {
                        playTimer = playTimerMax;
                        state = State.GamePlaying;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                    break;
                }
            case State.GamePlaying: 
                {
                    playTimer -= Time.deltaTime;

                    if (playTimer < 0f) {
                        state = State.GameOver;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                    break;
                }
            case State.GameOver: 
                {
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                    break;
                }
        }

    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountingDownToStart()
    {
        return state == State.CountDownToStart;
    }

    public float GetStartCountDown()
    {
        return countDownTimer;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetPlayTimeNormalized()
    {
        return 1 - (playTimer / playTimerMax);
    }
}
