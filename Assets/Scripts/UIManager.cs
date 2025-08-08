using UnityEngine;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private ScoreBoard _ScoreBoard;
    [SerializeField] private ValidWordsPanel _ValidWordsPanel;
    [SerializeField] private BoardContainer _BoardContainer;
    [SerializeField] private InfoMenu _InfoMenu;
    [SerializeField] private GameObject _DisablePanel;

    private void Awake()
    {
        if (Instance != null)
        {
            if (Instance != this)
                Destroy(this);
        }
        else
            Instance = this;
        _DisablePanel.SetActive(true);
    }

    public void UpdateScore(float total, float avg)
    {
        _ScoreBoard.SetTotalScoreText(total.ToString());
        _ScoreBoard.SetAverageScoreText(avg.ToString());
    }

    public void UpdateLevelsUIVisibility(bool enable)
    {
        _ScoreBoard.SetLevelsModeUI(enable);
    }

    public BoardContainer GetBoardContainer()
    {
        return _BoardContainer;
    }

    public ScoreBoard GetScoreBoard()
    {
        return _ScoreBoard;
    }

    public ValidWordsPanel GetValidWordsPanel()
    {
        return _ValidWordsPanel;
    }

    public void OnStartGame()
    {
        _DisablePanel.SetActive(false);
    }

    public void SetInfoMenuUI(GameMode gameMode)
    {
        switch(gameMode)
        {
            case GameMode.Endless:
                _InfoMenu.SetEndlessModeUI();
                break;
            case GameMode.Levels:
                _InfoMenu.SetLevelsModeUI();
                break;
            default:
                _InfoMenu.SetEndlessModeUI();
                break;
        }
    }
}