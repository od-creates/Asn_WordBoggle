using UnityEngine;
using System.Collections.Generic;

public class WordSelectionController : MonoBehaviour
{
    public static WordSelectionController Instance { get; private set; }

    [SerializeField] private WordHighlighter _WordHighlighter;

    private List<TileController> mSelected = new List<TileController>();
    private List<TileController> mSelectedCopy = null;

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

    private void TrySelect(Vector2 pos)
    {
        // Convert screen to world point on the camera-rendered area
        Vector3 wp3 = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, Mathf.Abs(Camera.main.transform.position.z)));
        Vector2 touchedPoint = wp3;

        // Check for any 2D collider overlapping that spot:
        Collider2D hitCollider = Physics2D.OverlapPoint(touchedPoint);
        if (hitCollider == null || GameManager.Instance.IsTimeUp())
            return;

        var tc = hitCollider.GetComponent<TileController>();
        if (tc != null && !tc.pIsLocked && tc.pType != TileType.Blocked)
        {
            if (mSelected.Count == 0 || (tc.IsAdjacentTo(mSelected[mSelected.Count - 1]) && !mSelected[mSelected.Count - 1].pIsLocked))//check validity for line draw
            {
                if (!mSelected.Contains(tc))//if new tile
                {
                    tc.Select();
                    mSelected.Add(tc);
                }
                else if (tc == mSelected[mSelected.Count - 2])//if second-last visited tile (retreating in the already visited path)
                {
                    mSelected[mSelected.Count - 1].Deselect();
                    mSelected.Remove(mSelected[mSelected.Count - 1]);
                }
                _WordHighlighter.DrawPath(mSelected);
            }
        }
    }

    //Fetch new tiles on valid word disappear
    private void FetchNewTiles(List<TileController> selected)
    {
        mSelectedCopy = new List<TileController>(selected);
        Invoke(nameof(FetchAndUpdateNewTilesData), 0.5f);
    }

    //Fetch new tiles and update them with next new data from the data file
    private void FetchAndUpdateNewTilesData()
    {
        var boardContainer = UIManager.Instance.GetBoardContainer();
        var newData = JSONLoader.LoadEndless();
        for (int i = 0; i < mSelectedCopy.Count; i++)
        {
            if (boardContainer.GetLastTileIndex() + i == newData.gridData.Length)
                boardContainer.UpdateLastTileIndex(-i);//loop again from the first
            var newTile = newData.gridData[boardContainer.GetLastTileIndex() + i];
            var gridPos = mSelectedCopy[i].pGridPosition;
            mSelectedCopy[i].Initialize(newTile.letter, newTile.tileType, gridPos);
            mSelectedCopy[i].gameObject.SetActive(true);
        }
        boardContainer.UpdateLastTileIndex(boardContainer.GetLastTileIndex() + mSelectedCopy.Count);
    }

    public void OnDragStart(Vector2 pos)
    {
        TrySelect(pos);
    }

    public void OnDrag(Vector2 pos)
    {
        TrySelect(pos);
    }

    public void OnDragEnd()
    {
        // build the word
        string word = string.Concat(mSelected.ConvertAll(t => t.pLetter));

        // clear tile highlights
        foreach (var t in mSelected)
            t.Deselect();

        // clear the swipe path
        _WordHighlighter.ClearPath();

        // validate & scoring
        if (DictionaryManager.Instance.IsValid(word) && UIManager.Instance.GetValidWordsPanel().IsUnusedValidWord(word))
        {
            int totalScore = 0;
            foreach (var t in mSelected)
            {
                int multiplier = t.pType == TileType.Bonus ? 2 : 1;
                totalScore += ScoreTable.GetValue(char.Parse(t.pLetter)) * multiplier;
            }
            ScoreManager.Instance.AddScore(totalScore);
            UIManager.Instance.GetValidWordsPanel().AddValidWord(word);
            if (GameManager.Instance.pMode == GameMode.Endless)
            {
                foreach (var t in mSelected)
                    t.Consume();
                FetchNewTiles(mSelected);
                GameManager.Instance.GetEndlessModeObj()?.OnWordFound(word);
            }
            else
            {
                foreach (var t in mSelected)
                    t.Locked();
                var levelsModeObj = GameManager.Instance.GetLevelsModeObj();
                levelsModeObj?.OnWordFound(word);
                levelsModeObj?.UnblockAdjacentBlockedTiles(mSelected, UIManager.Instance.GetBoardContainer().GetBlockedTileList());
            }
        }

        // reset for next drag
        mSelected.Clear();
    }

    public void OnResetWordSelection()
    {
        foreach (var t in mSelected)
            t.Deselect();
        _WordHighlighter.ClearPath();
        mSelected.Clear();
    }
}