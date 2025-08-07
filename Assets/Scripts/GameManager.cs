using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameMode mode;
    public LevelManager levelManager;
    public EndlessModeManager endlessManager;

    private bool isTimeUp = false;

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
                UIManager.Instance.UpdateLevelsUIVisibility(false);
                endlessManager.Init();
                break;
            case GameMode.Levels:
                UIManager.Instance.UpdateLevelsUIVisibility(true);
                levelManager.Init(0);//start from 1st level
                break;
        }
    }

    public void Reset()
    {
        var boardContainer = UIManager.Instance.GetBoardContainer();
        boardContainer.ClearTiles();
        boardContainer.ClearLastTileIndex();
        levelManager.DisableDisplayMsg();
        ScoreManager.Instance.ResetScore();
        UIManager.Instance.GetValidWordsPanel().ClearPanel();
    }

    public void SetTimeUp(bool timeUp)
    {
        isTimeUp = timeUp;
    }

    public bool IsTimeUp()
    {
        return isTimeUp;
    }
}