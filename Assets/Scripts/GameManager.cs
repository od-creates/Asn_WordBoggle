using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameMode mode;
    public int selectedLevelIndex = 0;
    public LevelManager levelManager;
    public EndlessModeManager endlessManager;

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

    public void StartGame()
    {
        Reset();
        switch (mode)
        {
            case GameMode.Endless:
                endlessManager.Init();
                UIManager.Instance.UpdateTimerVisibility(false);
                break;
            case GameMode.Levels:
                levelManager.Init(selectedLevelIndex);
                UIManager.Instance.UpdateTimerVisibility(true);
                break;
        }
    }

    public void Reset()
    {
        var boardContainer = UIManager.Instance.GetBoardContainer();
        boardContainer.ClearTiles();
        boardContainer.ClearLastTileIndex();
        ScoreManager.Instance.ResetScore();
        UIManager.Instance.GetValidWordsPanel().ClearPanel();
    }

    public int GetBoardWidth()
    {
        return UIManager.Instance.GetBoardContainer().GetBoardWidth();
    }

    public int GetBoardHeight()
    {
        return UIManager.Instance.GetBoardContainer().GetBoardHeight();
    }
}