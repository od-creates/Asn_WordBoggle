using UnityEngine;
using UnityEngine.UI;

public class BoardContainer : MonoBehaviour
{
    public GameObject letterTilePrefab;
    public GridLayoutGroup gridLayout;
    private TileController[,] grid;
    private int columns=0, rows=0;

    /// <summary>
    /// Builds grid from raw level data
    /// </summary>
    public void BuildGrid(LevelDataRaw data)
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
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                int idx = y * rows + x;
                var go = Instantiate(letterTilePrefab, gridLayout.transform);
                var tc = go.GetComponent<TileController>();
                tc.UnlockTilePosition();
                tc.Initialize(data.gridData[idx].letter, data.gridData[idx].tileType, new Vector2Int(x, y));
                grid[x, y] = tc;
            }
        }
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
        for(int i=0;i<gridLayout.transform.childCount;i++)
        {
            Destroy(gridLayout.transform.GetChild(i).gameObject);
        }

    }

    public int GetBoardWidth() => columns;
    public int GetBoardHeight() => rows;
    public TileController GetTile(int x, int y) => grid[x, y];
}

