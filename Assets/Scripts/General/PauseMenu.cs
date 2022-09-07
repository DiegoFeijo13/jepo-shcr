using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;
using System.Text;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused;
    
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private string MainMenu;    
    [SerializeField] private EnemyScore PlayerEnemyScore;
    [SerializeField] private TextMeshProUGUI EnemyScoreText;
    
    void Start()
    {
        isPaused = false;
        PausePanel.SetActive(isPaused);
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            ChangePause();
        }
    }

    public void ChangePause()
    {
        isPaused = !isPaused;
        PausePanel.SetActive(isPaused);

        if (isPaused)
        {
            OnOpenMenu();
            GameManager.Instance.SetPaused(true);
            Time.timeScale = 0f;
        }
        else
        {
            GameManager.Instance.SetPaused(false);
            Time.timeScale = 1f;
        }
    }

    public void QuitToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainMenu);
    }

    private void OnOpenMenu()
    {
        UpdateEnemyScore();
    }

    private void UpdateEnemyScore()
    {
        if(EnemyScoreText != null && 
            PlayerEnemyScore != null && 
            PlayerEnemyScore.Score != null &&
            PlayerEnemyScore.Score.Any()
            )
        {
            StringBuilder sb = new();
            foreach (var score in PlayerEnemyScore.Score)
            {
                sb.AppendLine($"{score.Enemy.GetDescription()}: {score.Count}");
            }

            EnemyScoreText.text = sb.ToString();
        }
    }
}
