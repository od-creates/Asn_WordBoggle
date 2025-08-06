using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI avgText;
    public TimerController timer;

    public void SetTotalScoreText(string totalScoreText)
    {
        scoreText.text = totalScoreText;
    }

    public void SetAverageScoreText(string avgScoreText)
    {
        avgText.text = avgScoreText;
    }

    public TimerController GetTimer()
    {
        return timer;
    }
}
