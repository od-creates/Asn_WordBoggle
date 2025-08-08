using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class WordHighlighter : MonoBehaviour
{
    [SerializeField] private GameObject _ArrowHeadPrefab;

    private LineRenderer mLine;
    private GameObject mArrowHead;

    private void Awake()
    {
        mLine = GetComponent<LineRenderer>();
        mLine.positionCount = 0;
        mLine.loop = false;
        mLine.useWorldSpace = true;

        if (_ArrowHeadPrefab != null)
        {
            mArrowHead = Instantiate(_ArrowHeadPrefab, transform);
            mArrowHead.SetActive(false);
        }
    }

    public void DrawPath(List<TileController> selectedTiles)
    {
        int count = selectedTiles.Count;
        mLine.positionCount = count;

        for (int i = 0; i < count; i++)
            mLine.SetPosition(i, selectedTiles[i].transform.position);

        // only show arrow if we have at least 2 points (visible line drawn)
        if (mArrowHead != null && count >= 2)
        {
            Vector3 tail = mLine.GetPosition(count - 2);
            Vector3 head = mLine.GetPosition(count - 1);
            Vector3 dir = (head - tail).normalized;

            mArrowHead.SetActive(true);
            mArrowHead.transform.position = head;

            // compute angle in degrees around Z axis
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            mArrowHead.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
            mArrowHead.SetActive(false);
    }

    public void ClearPath()
    {
        mLine.positionCount = 0;
        if (mArrowHead != null)
            mArrowHead.SetActive(false);
    }
}