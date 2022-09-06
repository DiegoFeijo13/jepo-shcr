using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private RDG rdg;

    private bool _isPaused;
    private string _gameOverScene = "GameOver";

    private static GameManager _instance;
    public static GameManager Instance {
        get
        {
            if (_instance == null)
                Debug.Log("GameManager is null");

            return _instance;
        }
    }

    internal void NextLevel()
    {
        rdg.NextLevel();
    }

    private void Awake()
    {
        _instance = this;
    }

    public void SetPaused(bool isPaused) => _isPaused = isPaused;
    public bool IsPaused() => _isPaused;

    public void GameOver()
    {
        SceneManager.LoadScene(_gameOverScene);
    }
}
