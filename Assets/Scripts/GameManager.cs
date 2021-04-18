using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 状態 </summary>
public enum GameState
{
    Start,
    SkillTime,
    Prepare,
    Playing,
    End,
    GameOver,
    Finish
}

public class GameManager : MonoBehaviour
{
    /// <summary> GameManagerのインスタンス </summary>
    public static GameManager Instance;

    /// <summary> 現在の状態 </summary>
    private GameState m_nowGameState;
    /// <summary> 現在の状態 </summary>
    public GameState NowGameState => m_nowGameState;
    void Awake()
    {
        Instance = this;
        SetNowState(GameState.Start);
    }

    /// <summary>
    /// このメソッドを使用し、状態を変更する
    /// </summary>
    /// <param name="state"></param>
    public void SetNowState(GameState state)
    {
        m_nowGameState = state;
        OnGameStateChanged(m_nowGameState);
    }

    /// <summary>
    /// 状態が変わったときの処理
    /// </summary>
    /// <param name="state"></param>
    void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                Debug.Log("GameState.Start");
                break;
            case GameState.SkillTime:
                Debug.Log("GameState.SkillTime");
                break;
            case GameState.Prepare:
                Debug.Log("GameState.Prepare");
                break;
            case GameState.Playing:
                Debug.Log("GameState.Playing");
                break;
            case GameState.End:
                Debug.Log("GameState.End");
                break;
            case GameState.GameOver:
                Debug.Log("GameState.GameOver");
                break;
            case GameState.Finish:
                Debug.Log("GameState.Finish");
                break;
            default:
                break;
        }
    }
}