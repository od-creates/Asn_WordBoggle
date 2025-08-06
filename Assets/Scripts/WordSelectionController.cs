using UnityEngine;
using System.Collections.Generic;

public class WordSelectionController : MonoBehaviour
{
    public static WordSelectionController Instance { get; private set; }

    [Header("Swipe Path")]
    [SerializeField] private WordHighlighter wordHighlighter;  // ① reference to your LineRenderer handler

    private List<TileController> selected = new List<TileController>();

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
                GameManager.Instance.endlessManager.OnWordFound(word);
            }
            else
                GameManager.Instance.levelManager.OnWordFound(word);
        }

        // reset for next drag
        selected.Clear();
    }
}