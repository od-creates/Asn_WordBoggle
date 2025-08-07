using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI avgText;
    public TimerController timer;
    public TextMeshProUGUI levelNoTxt;
    public TextMeshProUGUI minWordReqTxt;

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

    public void SetLevelsModeUI(bool enable)
    {
        timer.gameObject.SetActive(enable);
        levelNoTxt.transform.parent.gameObject.SetActive(enable);
        minWordReqTxt.transform.parent.gameObject.SetActive(enable);

    }

    public void UpdateLevelsModeUI(int levelNo, int minWordReq)
    {
        levelNoTxt.text = levelNo.ToString();
        minWordReqTxt.text = minWordReq.ToString();
    }
}
