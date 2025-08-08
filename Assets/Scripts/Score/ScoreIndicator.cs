using UnityEngine;

public class ScoreIndicator : MonoBehaviour
{
    [SerializeField] private GameObject _DotPrefab;
    [SerializeField] private Transform _DotsContainer;

    private TileController mTileController;

    private void Awake()
    {
        mTileController = transform.GetComponentInParent<TileController>();
    }

    private void ClearDots()
    {
        for (int i = _DotsContainer.childCount - 1; i >= 0; i--)
            Destroy(_DotsContainer.GetChild(i).gameObject);
    }

    /// <summary>
    /// Populates the dot container based on this tile's point value.
    /// </summary>
    public void RefreshDots()
    {
        ClearDots();
        int points = ScoreTable.GetValue(char.Parse(mTileController.pLetter));
        for (int i = 0; i < points; i++)
        {
            Instantiate(_DotPrefab, _DotsContainer);
        }
    }
}