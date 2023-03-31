using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerControler player;
    public UIManager uiManager;
    public AudioManager audioManager;

    public int score = 0, highScore = 0;

 

    private void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
        PlayerPrefs.Save();
    }


    public void StartGame()
    {
        player.StartRunning(true);
        audioManager.PlayGameBG();
    }

    public void EndGame()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }

        uiManager.ShowEndUI();
        audioManager.PlayAudio(2);

    }

    public void CollectCandy()
    {
        score += 10;
        audioManager.PlayAudio(0);
    }

    public void ReduceCandy()
    {
        score -= 10;
        audioManager.PlayAudio(1);
        if (score <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        audioManager.PlayAudio(2);
        uiManager.ShowGameOverUI();
    }
    public void ReloadGame()
    {
       SceneManager.LoadScene(0);
    }
}
