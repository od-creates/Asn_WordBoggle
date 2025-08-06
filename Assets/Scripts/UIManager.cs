using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public ScoreBoard scoreBoard;
    public ValidWordsPanel validWordsPanel;
    public BoardContainer boardContainer;
    void Awake()
    {
        if (Instance != null)
        {
            if (Instance != this)
                Destroy(this);
        }
        else
            Instance = this;
    }

    public void UpdateScore(float total, float avg)
    {
        scoreBoard.SetTotalScoreText(total.ToString());
        scoreBoard.SetAverageScoreText(avg.ToString());
    }
    public void UpdateTimer(int seconds)
    {
        var timer = scoreBoard.GetTimer();
        if (timer != null) timer.SetTime(seconds);
    }

    public void UpdateTimerVisibility(bool enable)
    {
        var timer = scoreBoard.GetTimer();
        if (timer != null) timer.gameObject.SetActive(enable);
    }

    public BoardContainer GetBoardContainer()
    {
        return boardContainer;
    }

    public ValidWordsPanel GetValidWordsPanel()
    {
        return validWordsPanel;
    }
}