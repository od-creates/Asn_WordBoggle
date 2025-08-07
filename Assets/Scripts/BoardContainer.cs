using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardContainer : MonoBehaviour
{
    public GameObject letterTilePrefab;
    public GridLayoutGroup gridLayout;

    private TileController[,] grid;
    private int columns = 0, rows = 0;
    private int lastTileIndex = 0;
    private List<TileController> blockedTiles = new List<TileController>();

    /// <summary>
    /// Builds grid from raw level data
    /// </summary>
    public void BuildGrid(DataRaw data)
    {
        columns = data.gridSize.x;
        rows = data.gridSize.y;
        var cellRt = letterTilePrefab.GetComponent<RectTransform>();
        float cellWidth = cellRt != null ? cellRt.rect.width : 0f;
        float cellHeight = cellRt != null ? cellRt.rect.height : 0f;
        Vector2 cellSize = new Vector2(cellWidth, cellHeight);
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = columns;
        var gridParentRt = gridLayout.transform.parent.GetComponent<RectTransform>();
        gridLayout.cellSize = cellSize;
        float gridWidth = columns * cellSize.x + (columns - 1) * gridLayout.spacing.x + gridLayout.padding.left + gridLayout.padding.right;
        float gridHeight = rows * cellSize.y + (rows - 1) * gridLayout.spacing.y + gridLayout.padding.top + gridLayout.padding.bottom;
        gridParentRt.sizeDelta = new Vector2(gridWidth, gridHeight);

        grid = new TileController[rows, columns];
        if(data.gridData.Length<(rows*columns))
        {
            Debug.LogError("Grid size is greater than Grid data count!");
            return;
        }
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                int idx = x * columns + y;
                var go = Instantiate(letterTilePrefab, gridLayout.transform);
                var tc = go.GetComponent<TileController>();
                tc.UnlockTilePosition();
                tc.Initialize(data.gridData[idx].letter, data.gridData[idx].tileType, new Vector2Int(x, y));
                if (data.gridData[idx].tileType == TileType.Blocked)
                    blockedTiles.Add(tc);
                grid[x, y] = tc;
            }
        }
        lastTileIndex = columns * rows;
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(gridLayout.GetComponent<RectTransform>());
        LockTilePositions();
    }

    private void LockTilePositions()
    {
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                grid[x, y].LockTilePosition();
            }
        }
    }

    public void ClearTiles()
    {
        for (int i = 0; i < gridLayout.transform.childCount; i++)
        {
            Destroy(gridLayout.transform.GetChild(i).gameObject);
        }
        blockedTiles.Clear();
    }
    
    public int GetLastTileIndex()
    {
        return lastTileIndex;
    }

    public void UpdateLastTileIndex(int newIndex)
    {
        lastTileIndex = newIndex;
    }

    public void ClearLastTileIndex()
    {
        lastTileIndex = 0;
    }

    public void UnlockTileAndUpdateList(TileController tile)
    {
        tile.UnblockTile();
        blockedTiles.Remove(tile);
    }

    public List<TileController> GetBlockedTileList() => blockedTiles;
    public int GetBoardWidth() => columns;
    public int GetBoardHeight() => rows;
    public TileController GetTile(int x, int y) => grid[x, y];
}

