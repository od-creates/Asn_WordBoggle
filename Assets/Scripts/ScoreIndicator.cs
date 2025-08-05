using UnityEngine;

[RequireComponent(typeof(TileController))]
public class ScoreIndicator : MonoBehaviour
{
    [Header("Dot Settings")]
    [Tooltip("Prefab of the single dot icon.")]
    [SerializeField] private GameObject dotPrefab;

    [Tooltip("Container under the tile where dots will be instantiated.")]
    [SerializeField] private Transform dotsContainer;

    private TileController tileController;

    void Awake()
    {
        tileController = GetComponent<TileController>();
    }

    /// <summary>
    /// Populates the dot container based on this tile's point value.
    /// Call this after Initialize() on the TileController.
    /// </summary>
    public void RefreshDots()
    {
        ClearDots();
        int points = ScoreTable.GetValue(char.Parse(tileController.Letter));
        for (int i = 0; i < points; i++)
        {
            Instantiate(dotPrefab, dotsContainer);
        }
    }

    private void ClearDots()
    {
        for (int i = dotsContainer.childCount - 1; i >= 0; i--)
            Destroy(dotsContainer.GetChild(i).gameObject);
    }
}