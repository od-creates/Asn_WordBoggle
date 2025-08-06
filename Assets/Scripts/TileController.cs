using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class TileController : MonoBehaviour
{
    public TextMeshProUGUI letterText = null;
    public GameObject highlight = null;
    public GameObject bonusObj = null;
    public GameObject blockObj = null;

    public string Letter { get; private set; }
    public TileType Type { get; private set; }
    public Vector2Int GridPosition { get; private set; }
    void Awake()
    {
        highlight.SetActive(false);
        bonusObj.SetActive(false);
        blockObj.SetActive(false);
    }
    public void Initialize(string letter, TileType type, Vector2Int pos)
    {
        Letter = letter; Type = type; GridPosition = pos;
        letterText.text = letter;
        switch(type)
        {
            case TileType.Bonus:
                bonusObj.SetActive(true);
                blockObj.SetActive(false);
                break;
            case TileType.Blocked:
                bonusObj.SetActive(false);
                blockObj.SetActive(true);
                break;
            default:
                bonusObj.SetActive(false);
                blockObj.SetActive(false);
                break;
        }
    }

    public void LockTilePosition()
    {
        GetComponent<LayoutElement>().ignoreLayout = true;
    }

    public void UnlockTilePosition()
    {
        GetComponent<LayoutElement>().ignoreLayout = false;
    }

    public void Select()
    {
        highlight.SetActive(true);
    }
    public void Deselect()
    {
        highlight.SetActive(false);
    }
    public void Consume()
    {
        if (Type != TileType.Blocked)
            gameObject.SetActive(false);
    }
    public bool IsAdjacentTo(TileController other)
    {
        bool adjLinear=false, adjDiagonal=false;
        if (Mathf.Abs(GridPosition.x - other.GridPosition.x) + Mathf.Abs(GridPosition.y - other.GridPosition.y) == 1)
            adjLinear = true;
        else if (Mathf.Abs(GridPosition.x - other.GridPosition.x) == 1 && Mathf.Abs(GridPosition.y - other.GridPosition.y) == 1)
            adjDiagonal = true;
        return adjLinear || adjDiagonal;
    }
}
