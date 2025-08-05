using UnityEngine;
using System.Collections.Generic;
public class WordSelectionController : MonoBehaviour
{
    public static WordSelectionController Instance { get; private set; }
    private List<TileController> selected = new List<TileController>();
    void Awake() => Instance = this;
    public void OnDragStart(Vector2 pos) { TrySelect(pos); }
    public void OnDrag(Vector2 pos) { TrySelect(pos); }
    void TrySelect(Vector2 pos)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
        if (hit.collider != null)
        {
            var tc = hit.collider.GetComponent<TileController>();
            if (tc != null && !selected.Contains(tc) && selected.Count < 16)
            {
                if (selected.Count == 0 || tc.IsAdjacentTo(selected[selected.Count - 1]))
                {
                    selected.Add(tc);
                    tc.Select();
                }
            }
        }
    }
    public void OnDragEnd()
    {
        string word = string.Concat(selected.ConvertAll(t => t.Letter));
        foreach (var t in selected) t.Deselect();
        if (DictionaryManager.Instance.IsValid(word))
        {
            ScoreManager.Instance.AddScore(word.Length);
            foreach (var t in selected) t.Consume();
            if (GameManager.Instance.mode == GameMode.Endless)
                GameManager.Instance.endlessManager.OnWordFound(word);
            else
                GameManager.Instance.levelManager.OnWordFound(word);
        }
        selected.Clear();
    }
}
