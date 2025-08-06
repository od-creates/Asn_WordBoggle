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

    void TrySelect(Vector2 pos)
    {
        // Convert screen → world on the tile plane:
        Vector3 wp3 = Camera.main.ScreenToWorldPoint(
            new Vector3(pos.x, pos.y, Mathf.Abs(Camera.main.transform.position.z))
        );
        Vector2 samplePoint = wp3;
        Debug.Log($"Sampling at: {samplePoint}");

        Debug.DrawLine(samplePoint + Vector2.up * 0.1f,
               samplePoint - Vector2.up * 0.1f,
               Color.red, 1f);
        Debug.DrawLine(samplePoint + Vector2.right * 0.1f,
                       samplePoint - Vector2.right * 0.1f,
                       Color.red, 1f);

        // Point-check for any 2D collider overlapping that spot:
        Collider2D hitCollider = Physics2D.OverlapPoint(samplePoint);
        if (hitCollider == null)
            return;

        var tc = hitCollider.GetComponent<TileController>();
        int maxTiles = GameManager.Instance.GetBoardWidth() * GameManager.Instance.GetBoardHeight();
        if (tc != null && !selected.Contains(tc) && selected.Count < maxTiles)
        {
            if (selected.Count == 0 || tc.IsAdjacentTo(selected[selected.Count - 1]))
            {
                selected.Add(tc);
                tc.Select();
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
            ScoreManager.Instance.AddScore(word.Length);
            UIManager.Instance.GetValidWordsPanel().AddValidWord(word);

            if (GameManager.Instance.mode == GameMode.Endless)
            {
                foreach (var t in selected)
                    t.Consume();
                FetchNewTiles(selected);
                GameManager.Instance.endlessManager.OnWordFound(word);
            }
            else
                GameManager.Instance.levelManager.OnWordFound(word);
        }

        // reset for next drag
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