using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class WordHighlighter : MonoBehaviour
{
    private LineRenderer _line;

    void Awake()
    {
        _line = GetComponent<LineRenderer>();
        _line.positionCount = 0;            // start with no segments
        _line.loop = false;                 // don’t close the path
        _line.useWorldSpace = true;         // world positions of tiles
    }

    /// <summary>
    /// Call this each time the player adds a new tile to the selection.
    /// </summary>
    /// <param name="selectedTiles">Ordered list of currently selected tiles</param>
    public void DrawPath(List<TileController> selectedTiles)
    {
        Debug.LogError("Start Draw");
        int count = selectedTiles.Count;
        _line.positionCount = count;
        for (int i = 0; i < count; i++)
        {
            // Use the tile’s world position (you can offset in z if needed)
            _line.SetPosition(i, selectedTiles[i].transform.position);
        }
    }

    /// <summary>
    /// Clears the current path (e.g. on drag-end or invalid word).
    /// </summary>
    public void ClearPath()
    {
        _line.positionCount = 0;
    }
}