using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public ScoreBoard scoreBoard;
    public ValidWordsPanel validWordsPanel;
    public BoardContainer boardContainer;
    public InfoMenu infoMenu;
    public GameObject disablePanel;
    void Awake()
    {
        if (Instance != null)
        {
            if (Instance != this)
                Destroy(this);
        }
        else
            Instance = this;
        disablePanel.SetActive(true);
    }

    public void UpdateScore(float total, float avg)
    {
        scoreBoard.SetTotalScoreText(total.ToString());
        scoreBoard.SetAverageScoreText(avg.ToString());
    }

    public void UpdateLevelsUIVisibility(bool enable)
    {
        scoreBoard.SetLevelsModeUI(enable);
    }

    public BoardContainer GetBoardContainer()
    {
        return boardContainer;
    }

    public ValidWordsPanel GetValidWordsPanel()
    {
        return validWordsPanel;
    }

    public void OnStartGame()
    {
        disablePanel.SetActive(false);
    }

    public void SetInfoMenuUI(GameMode gameMode)
    {
        switch(gameMode)
        {
            case GameMode.Endless:
                infoMenu.SetEndlessModeUI();
                break;
            case GameMode.Levels:
                infoMenu.SetLevelsModeUI();
                break;
            default:
                infoMenu.SetEndlessModeUI();
                break;
        }
    }
}