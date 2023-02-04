using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using GameAnalyticsSDK;

public class GameManager : MonoSingleton<GameManager>
{
    #region Fields And Properties

    [SerializeField] int levelCount;
    [SerializeField] int randomLevelLowerLimit;
    [SerializeField] int goldCoefficient;

    public int PlayerWinCount
    {
        get
        {
            return PlayerPrefs.GetInt("PlayerWinCount", 0);
        }
        set
        {
            PlayerPrefs.SetInt("PlayerWinCount", value);
        }
    }

    public int AiWinCount
    {
        get
        {
            return PlayerPrefs.GetInt("AiWinCount", 0);
        }
        set
        {
            PlayerPrefs.SetInt("AiWinCount", value);
        }
    }

    private EventData _eventData;
    GameState _gameState = GameState.Play;

    public GameState GameState
    {
        get => _gameState;
        set => _gameState = value;
    }


    #endregion

    #region MonoBehaviour Methods

    private void Awake()
    {
        Singleton(true);
        _eventData = Resources.Load("EventData") as EventData;
    }

    private void OnEnable()
    {
        _eventData.OnFinish += Finish;
    }

    private void OnDisable()
    {
    }

    #endregion

    #region Listening Methods

    private void Finish(bool statu)
    {
        GameState = GameState.Finish;

        if (statu)
        {
            PlayerWinCount++;
        }
        else
        {
            AiWinCount++;
        }
    }

    #endregion

    #region Unique Methods

    public bool Playability()
    {
        return _gameState == GameState.Play;
    }

    public void NextLevel()
    {
        _gameState = GameState.Play;
    }

    #endregion


}
