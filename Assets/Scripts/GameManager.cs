using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [HideInInspector] public GameMode pMode;
    [SerializeField] private LevelModeManager _LevelModeManager;
    [SerializeField] private EndlessModeManager _EndlessManager;

    private bool mIsTimeUp = false;

    private void Awake()
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
        ResetUI();
        ScoreManager.Instance.ResetTotalWordCountAndScore();
        switch (pMode)
        {
            case GameMode.Endless:
                UIManager.Instance.UpdateLevelsUIVisibility(false);
                _EndlessManager.Init();
                break;
            case GameMode.Levels:
                UIManager.Instance.UpdateLevelsUIVisibility(true);
                _LevelModeManager.Init(0);//start from 1st level
                break;
        }
    }

    public EndlessModeManager GetEndlessModeObj()
    {
        return _EndlessManager;
    }

    public LevelModeManager GetLevelsModeObj()
    {
        return _LevelModeManager;
    }

    public void ResetUI()
    {
        var boardContainer = UIManager.Instance.GetBoardContainer();
        boardContainer.ClearTiles();
        boardContainer.ClearLastTileIndex();
        _LevelModeManager.DisableDisplayMsg();
        ScoreManager.Instance.ResetLevelWordCountAndScore();
        UIManager.Instance.GetValidWordsPanel().ClearPanel();
    }

    public void SetTimeUp(bool timeUp)
    {
        mIsTimeUp = timeUp;
    }

    public bool IsTimeUp()
    {
        return mIsTimeUp;
    }
}