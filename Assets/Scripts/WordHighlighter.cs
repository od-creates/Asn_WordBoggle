using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class WordHighlighter : MonoBehaviour
{
    [Header("Arrow Settings")]
    [Tooltip("Prefab of a small arrow pointing along +X")]
    [SerializeField] private GameObject arrowHeadPrefab;

    private LineRenderer _line;
    private GameObject _arrowHead;

    void Awake()
    {
        _line = GetComponent<LineRenderer>();
        _line.positionCount = 0;
        _line.loop = false;
        _line.useWorldSpace = true;

        // Instantiate arrow but deactivate initially
        if (arrowHeadPrefab != null)
        {
            _arrowHead = Instantiate(arrowHeadPrefab, transform);
            _arrowHead.SetActive(false);
        }
    }

    public void DrawPath(List<TileController> selectedTiles)
    {
        int count = selectedTiles.Count;
        _line.positionCount = count;

        for (int i = 0; i < count; i++)
            _line.SetPosition(i, selectedTiles[i].transform.position);

        // only show arrow if we have at least 2 points
        if (_arrowHead != null && count >= 2)
        {
            Vector3 tail = _line.GetPosition(count - 2);
            Vector3 head = _line.GetPosition(count - 1);
            Vector3 dir = (head - tail).normalized;

            _arrowHead.SetActive(true);
            _arrowHead.transform.position = head;

            // compute angle in degrees around Z axis
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            _arrowHead.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public void ClearPath()
    {
        _line.positionCount = 0;
        if (_arrowHead != null)
            _arrowHead.SetActive(false);
    }
}