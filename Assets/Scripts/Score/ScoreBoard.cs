using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ScoreText;
    [SerializeField] private TextMeshProUGUI _AvgText;
    [SerializeField] private TimerController _Timer;
    [SerializeField] private TextMeshProUGUI _LevelNoTxt;
    [SerializeField] private TextMeshProUGUI _MinWordReqTxt;

    public void SetTotalScoreText(string totalScoreText)
    {
        _ScoreText.text = totalScoreText;
    }

    public void SetAverageScoreText(string avgScoreText)
    {
        _AvgText.text = avgScoreText;
    }

    public TimerController GetTimer()
    {
        return _Timer;
    }

    public void SetLevelsModeUI(bool enable)
    {
        _Timer.gameObject.SetActive(enable);
        _LevelNoTxt.transform.parent.gameObject.SetActive(enable);
        _MinWordReqTxt.transform.parent.gameObject.SetActive(enable);

    }

    public void UpdateLevelsModeUI(int levelNo, int minWordReq)
    {
        _LevelNoTxt.text = levelNo.ToString();
        _MinWordReqTxt.text = minWordReq.ToString();
    }
}
