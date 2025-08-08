using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardContainer : MonoBehaviour
{
    [SerializeField] private GameObject _LetterTilePrefab;
    [SerializeField] private GridLayoutGroup _GridLayout;

    private TileController[,] mGrid;
    private int mColumns = 0, mRows = 0;
    private int mLastTileIndex = 0;
    private List<TileController> mBlockedTiles = new List<TileController>();

    private void LockTilePositions()
    {
        for (int x = 0; x < mRows; x++)
        {
            for (int y = 0; y < mColumns; y++)
            {
                mGrid[x, y].LockTilePosition();
            }
        }
    }

    /// <summary>
    /// Builds grid from raw level data
    /// </summary>
    public void BuildGrid(DataRaw data)
    {
        mColumns = data.gridSize.x;
        mRows = data.gridSize.y;
        var cellRt = _LetterTilePrefab.GetComponent<RectTransform>();
        float cellWidth = cellRt != null ? cellRt.rect.width : 0f;
        float cellHeight = cellRt != null ? cellRt.rect.height : 0f;
        Vector2 cellSize = new Vector2(cellWidth, cellHeight);
        _GridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _GridLayout.constraintCount = mColumns;
        var gridParentRt = _GridLayout.transform.parent.GetComponent<RectTransform>();
        _GridLayout.cellSize = cellSize;
        float gridWidth = mColumns * cellSize.x + (mColumns - 1) * _GridLayout.spacing.x + _GridLayout.padding.left + _GridLayout.padding.right;
        float gridHeight = mRows * cellSize.y + (mRows - 1) * _GridLayout.spacing.y + _GridLayout.padding.top + _GridLayout.padding.bottom;
        gridParentRt.sizeDelta = new Vector2(gridWidth, gridHeight);

        mGrid = new TileController[mRows, mColumns];
        if(data.gridData.Length<(mRows*mColumns))
        {
            Debug.LogError("Grid size is greater than Grid data count!");
            return;
        }
        for (int x = 0; x < mRows; x++)
        {
            for (int y = 0; y < mColumns; y++)
            {
                int idx = x * mColumns + y;
                var go = Instantiate(_LetterTilePrefab, _GridLayout.transform);
                var tc = go.GetComponent<TileController>();
                tc.UnlockTilePosition();
                tc.Initialize(data.gridData[idx].letter, data.gridData[idx].tileType, new Vector2Int(x, y));
                if (data.gridData[idx].tileType == TileType.Blocked)
                    mBlockedTiles.Add(tc);
                mGrid[x, y] = tc;
            }
        }
        mLastTileIndex = mColumns * mRows;

        //Updating canvas layout before locking tile positions
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(_GridLayout.GetComponent<RectTransform>());

        //Locking tile positions
        LockTilePositions();
    }

    /// <summary>
    /// Clear all tiles from board
    /// </summary>
    public void ClearTiles()
    {
        for (int i = 0; i < _GridLayout.transform.childCount; i++)
        {
            Destroy(_GridLayout.transform.GetChild(i).gameObject);
        }
        mBlockedTiles.Clear();
    }

    /// <summary>
    /// Get the index of the last used tile data
    /// </summary>
    public int GetLastTileIndex()
    {
        return mLastTileIndex;
    }

    /// <summary>
    /// Update the index of the last used tile data
    /// </summary>
    public void UpdateLastTileIndex(int newIndex)
    {
        mLastTileIndex = newIndex;
    }

    /// <summary>
    /// Reset the index of the last used tile data
    /// </summary>
    public void ClearLastTileIndex()
    {
        mLastTileIndex = 0;
    }

    public void UnblockTileAndUpdateList(TileController tile)
    {
        tile.UnblockTile();
        mBlockedTiles.Remove(tile);
    }

    public List<TileController> GetBlockedTileList() => mBlockedTiles;
    public int GetBoardWidth() => mColumns;
    public int GetBoardHeight() => mRows;
    public TileController GetTile(int x, int y) => mGrid[x, y];
}

