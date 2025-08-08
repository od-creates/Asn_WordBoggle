using System;
using System.Collections.Generic;
using UnityEngine;
public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject levelCleared;
    [SerializeField] private GameObject levelRetry;

    private DataRaw[] levels;
    private int currentLevelIndex = 0;
    private int currentLevelWordCount = 0;

    void Awake()
    {
        levels = JSONLoader.LoadLevels();
        DisableDisplayMsg();
    }

    /// <summary>
    /// Initialize given level index
    /// </summary>
    public void Init(int levelIndex)
    {
        GameManager.Instance.SetTimeUp(false);
        DisableDisplayMsg();
        if (levels == null || levels.Length == 0) return;
        currentLevelIndex = levelIndex % levels.Length;//wrap around
        DataRaw lvl = levels[currentLevelIndex];
        currentLevelWordCount = lvl.wordCount;
        UIManager.Instance.GetBoardContainer().BuildGrid(lvl);
        var scoreBoard = UIManager.Instance.scoreBoard;
        var timer = scoreBoard.GetTimer();
        if (timer != null && lvl.timeSec > 0)
        {
            timer.SetTime(lvl.timeSec);
            timer.OnTimerFinished += HandleTimeUp;
            timer.StartTimer();
        }
        scoreBoard.UpdateLevelsModeUI(currentLevelIndex + 1, currentLevelWordCount);
    }

    private void HandleTimeUp()
    {
        GameManager.Instance.SetTimeUp(true);
        WordSelectionController.Instance.OnResetWordSelection();
        var timer = UIManager.Instance.scoreBoard.GetTimer();
        if (timer != null)
            timer.OnTimerFinished -= HandleTimeUp;
        int validWordCount = (int)ScoreManager.Instance.GetLevelWordCount();
        if (validWordCount >= currentLevelWordCount)
            Invoke(nameof(ShowWinMsg), 1f);
        else
            Invoke(nameof(ShowRetryMsg), 1f);
    }

    private void ShowWinMsg()
    {
        GameManager.Instance.ResetUI();
        levelCleared.SetActive(true);
        levelRetry.SetActive(false);
    }

    private void ShowRetryMsg()
    {
        GameManager.Instance.ResetUI();
        levelCleared.SetActive(false);
        levelRetry.SetActive(true);
    }

    public void OnClickPlayNext()
    {
        currentLevelIndex++;
        Init(currentLevelIndex);
    }

    public void OnClickRetry()
    {
        Init(currentLevelIndex);
    }

    public void DisableDisplayMsg()
    {
        levelCleared.SetActive(false);
        levelRetry.SetActive(false);
    }

    public void OnWordFound(string word)
    {
        // track objectives per level
    }

    public void UnblockAdjacentBlockedTiles(List<TileController> selectedTileList, List<TileController> blockedTileList)
    {
        var blockedTileListCopy = new List<TileController>(blockedTileList);
        foreach(var blockedTile in blockedTileListCopy)
        {
            foreach(var selectedTile in selectedTileList)
            {
                if (blockedTile.IsAdjacentTo(selectedTile))
                    UIManager.Instance.GetBoardContainer().UnlockTileAndUpdateList(blockedTile);
            }
        }
    }
}