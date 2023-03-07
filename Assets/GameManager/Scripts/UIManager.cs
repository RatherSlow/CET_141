using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI ScoreValue;
    [SerializeField]
    TextMeshProUGUI TimeValue;
    [SerializeField]
    GameObject GameOverPanel;
    [SerializeField]
    TextMeshProUGUI EndScoreValue;
    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreUI(0);
        UpdateTimeUI(0);        
    }

    public void UpdateScoreUI(int value)
    {
        // "D5" - minimum of 5 digits, preceding shorter numbers with 0s
        ScoreValue.text = value.ToString("D5");
    }

    public void UpdateTimeUI(float time)
    {
        int seconds = (int)time;
        TimeValue.text = System.TimeSpan.FromSeconds(seconds).ToString("hh':'mm':'ss");
    }
    
    public void ActivateEndGame(int score)
    {
        EndScoreValue.text = score.ToString();
        GameOverPanel.SetActive(true);
        Cursor.visible = true;
    }
}
