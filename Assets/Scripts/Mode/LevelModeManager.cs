using System;
using System.Collections.Generic;
using UnityEngine;
public class LevelModeManager : MonoBehaviour
{
    [SerializeField] private GameObject _LevelCleared;
    [SerializeField] private GameObject _LevelRetry;

    private DataRaw[] mLevels;
    private int mCurrentLevelIndex = 0;
    private int mCurrentLevelWordCount = 0;

    private void Awake()
    {
        mLevels = JSONLoader.LoadLevels();
        DisableDisplayMsg();
    }

    //Handling on Timer exhaused
    private void HandleTimeUp()
    {
        GameManager.Instance.SetTimeUp(true);
        WordSelectionController.Instance.OnResetWordSelection();
        var timer = UIManager.Instance.GetScoreBoard().GetTimer();
        if (timer != null)
            timer.pOnTimerFinished -= HandleTimeUp;
        int validWordCount = (int)ScoreManager.Instance.GetLevelWordCount();
        if (validWordCount >= mCurrentLevelWordCount)
            Invoke(nameof(ShowWinMsg), 1f);
        else
            Invoke(nameof(ShowRetryMsg), 1f);
    }

    // Display Level wise win message
    private void ShowWinMsg()
    {
        GameManager.Instance.ResetUI();
        _LevelCleared.SetActive(true);
        _LevelRetry.SetActive(false);
    }

    // Display Level wise retry message
    private void ShowRetryMsg()
    {
        var updatedScore = ScoreManager.Instance.GetTotalScore() - ScoreManager.Instance.GetLevelScore();
        var updatedWordCount = ScoreManager.Instance.GetTotalWordCount() - ScoreManager.Instance.GetLevelWordCount();
        ScoreManager.Instance.UpdateTotalScoreAndWordCount(updatedScore, updatedWordCount);
        GameManager.Instance.ResetUI();
        _LevelCleared.SetActive(false);
        _LevelRetry.SetActive(true);
    }

    /// <summary>
    /// Initialize with level data
    /// </summary>
    public void Init(int levelIndex)
    {
        GameManager.Instance.SetTimeUp(false);
        DisableDisplayMsg();
        if (mLevels == null || mLevels.Length == 0) return;
        mCurrentLevelIndex = levelIndex % mLevels.Length;//wrap around
        DataRaw lvl = mLevels[mCurrentLevelIndex];
        mCurrentLevelWordCount = lvl.wordCount;
        UIManager.Instance.GetBoardContainer().BuildGrid(lvl);
        var scoreBoard = UIManager.Instance.GetScoreBoard();
        var timer = scoreBoard.GetTimer();
        if (timer != null && lvl.timeSec > 0)
        {
            timer.SetTime(lvl.timeSec);
            timer.pOnTimerFinished += HandleTimeUp;
            timer.StartTimer();
        }
        scoreBoard.UpdateLevelsModeUI(mCurrentLevelIndex + 1, mCurrentLevelWordCount);
    }

    public void OnClickPlayNext()
    {
        mCurrentLevelIndex++;
        Init(mCurrentLevelIndex);
    }

    public void OnClickRetry()
    {
        Init(mCurrentLevelIndex);
    }

    public void DisableDisplayMsg()
    {
        _LevelCleared.SetActive(false);
        _LevelRetry.SetActive(false);
    }

    public void UnblockAdjacentBlockedTiles(List<TileController> selectedTileList, List<TileController> blockedTileList)
    {
        var blockedTileListCopy = new List<TileController>(blockedTileList);
        foreach (var blockedTile in blockedTileListCopy)
        {
            foreach (var selectedTile in selectedTileList)
            {
                if (blockedTile.IsAdjacentTo(selectedTile))
                    UIManager.Instance.GetBoardContainer().UnblockTileAndUpdateList(blockedTile);
            }
        }
    }

    public void OnWordFound(string word)
    {
        // track objectives per level
    }
}