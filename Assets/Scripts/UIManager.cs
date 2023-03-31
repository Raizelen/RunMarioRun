using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameManager manager;

    public GameObject startScreen, endScreen, gameOverScreen;

    public TMP_Text currentScore,FinalScore,HighScore;

    private void Start()
    {
        startScreen.SetActive(true);
    }

    private void Update()
    {
        currentScore.text = manager.score.ToString();
    }

    public void OnPlayButtonClick()
    {
        manager.StartGame();
        startScreen.SetActive(false);
    }

    public void ShowEndUI()
    {
        FinalScore.text = currentScore.text;
        HighScore.text = manager.highScore.ToString();
        endScreen.SetActive(true);
    }

    public void ShowGameOverUI()
    {
        gameOverScreen.SetActive(true);
    }
}
