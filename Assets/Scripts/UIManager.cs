using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI avgText;
    public TimerController timer;
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

    public void UpdateScore(int total, int avg)
    {
        scoreText.text = total.ToString();
        avgText.text = avg.ToString();
    }
    public void UpdateTime(int seconds)
    {
        if (timer != null) timer.SetTime(seconds);
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