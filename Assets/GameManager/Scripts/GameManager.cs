using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // DESIGN PATTERN: SINGLETON
    public static GameManager instance {  get; private set; }
    public UIManager UIManager { get; private set; }
    public HighScoreSystem HighScoreSystem { get; private set; }

    private static float secondsSinceStart = 0;
    private static int score;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;

        UIManager = GetComponent<UIManager>();
        HighScoreSystem = GetComponent<HighScoreSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        secondsSinceStart += Time.deltaTime;
        instance.UIManager.UpdateTimeUI(secondsSinceStart);
    }

    public static string GetScoreText()
    {
        return score.ToString();
    }

    public static void IncrementScore(int value)
    {
        score += value;
        instance.UIManager.UpdateScoreUI(score);
        Debug.Log("Score: " + score);
    }

    public static void ResetGame()
    {
        ResetScore();
        secondsSinceStart = 0f;
        Time.timeScale = 1f;
    }

    private static void ResetScore()
    {
        score = 0;
        instance.UIManager.UpdateScoreUI(score);
        Debug.Log("Score: " + score);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        MenuController.IsGamePaused = true;

        instance.UIManager.ActivateEndGame(score);
        HighScoreSystem.CheckHighScore("Anon", score);
    }
}
