using UnityEngine;
using System.Collections.Generic;

public class WordSelectionController : MonoBehaviour
{
    public static WordSelectionController Instance { get; private set; }

    [Header("Swipe Path")]
    [SerializeField] private WordHighlighter wordHighlighter;  // ① reference to your LineRenderer handler

    private List<TileController> selected = new List<TileController>();
    private List<TileController> selectedCopy = null;

    void Awake()
    {
        Instance = this;
        // fallback if you forgot to wire it up in the Inspector:
        if (wordHighlighter == null)
            wordHighlighter = FindObjectOfType<WordHighlighter>();
    }

    public void OnDragStart(Vector2 pos)
    {
        TrySelect(pos);
    }

    public void OnDrag(Vector2 pos)
    {
        TrySelect(pos);
    }

    private void TrySelect(Vector2 pos)
    {
        // Convert screen → world on the tile plane:
        Vector3 wp3 = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, Mathf.Abs(Camera.main.transform.position.z)));
        Vector2 touchedPoint = wp3;

        // Point-check for any 2D collider overlapping that spot:
        Collider2D hitCollider = Physics2D.OverlapPoint(touchedPoint);
        if (hitCollider == null || GameManager.Instance.IsTimeUp())
            return;

        var tc = hitCollider.GetComponent<TileController>();
        if (tc != null && !tc.IsLocked && tc.Type != TileType.Blocked)
        {
            if (selected.Count == 0 || tc.IsAdjacentTo(selected[selected.Count - 1]))
            {
                if (!selected.Contains(tc))
                {
                    tc.Select();
                    selected.Add(tc);
                }
                else if (tc == selected[selected.Count - 2])
                {
                    selected[selected.Count - 1].Deselect();
                    selected.Remove(selected[selected.Count - 1]);
                }
                wordHighlighter.DrawPath(selected);
            }
        }
    }

    public void OnDragEnd()
    {
        // build the word
        string word = string.Concat(selected.ConvertAll(t => t.Letter));

        // clear tile highlights
        foreach (var t in selected)
            t.Deselect();

        // clear the swipe path
        wordHighlighter.ClearPath();  // ③

        // validate & score
        if (DictionaryManager.Instance.IsValid(word))
        {
            int totalScore = 0;
            foreach (var t in selected)
            {
                int multiplier = t.Type == TileType.Bonus ? 2 : 1;
                totalScore += ScoreTable.GetValue(char.Parse(t.Letter)) * multiplier;
            }

            ScoreManager.Instance.AddScore(totalScore);
            UIManager.Instance.GetValidWordsPanel().AddValidWord(word);
            if (GameManager.Instance.mode == GameMode.Endless)
            {
                foreach (var t in selected)
                    t.Consume();
                FetchNewTiles(selected);
                GameManager.Instance.endlessManager.OnWordFound(word);
            }
            else
            {
                foreach (var t in selected)
                    t.Locked();
                GameManager.Instance.levelManager.OnWordFound(word);
            }
        }

        // reset for next drag
        selected.Clear();
    }

    public void OnResetWordSelection()
    {
        foreach (var t in selected)
            t.Deselect();
        wordHighlighter.ClearPath();
        selected.Clear();
    }

    private void FetchNewTiles(List<TileController> selected)
    {
        selectedCopy = new List<TileController>(selected);
        Invoke(nameof(FetchAndUpdateNewTilesData), 0.5f);
    }

    private void FetchAndUpdateNewTilesData()
    {
        var boardContainer = UIManager.Instance.GetBoardContainer();
        var newData = JSONLoader.LoadEndless();
        for (int i = 0; i < selectedCopy.Count; i++)
        {
            if (boardContainer.GetLastTileIndex() + i == newData.gridData.Length)
                boardContainer.UpdateLastTileIndex(-i);//loop again from the first
            var newTile = newData.gridData[boardContainer.GetLastTileIndex() + i];
            var gridPos = selectedCopy[i].GridPosition;
            selectedCopy[i].Initialize(newTile.letter, newTile.tileType, gridPos);
            selectedCopy[i].gameObject.SetActive(true);
        }
        boardContainer.UpdateLastTileIndex(boardContainer.GetLastTileIndex() + selectedCopy.Count);
    }
}