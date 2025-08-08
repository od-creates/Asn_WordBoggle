using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class TileController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _LetterText = null;
    [SerializeField] private GameObject _Highlight = null;
    [SerializeField] private GameObject _Locked = null;
    [SerializeField] private GameObject _BonusObj = null;
    [SerializeField] private GameObject _BlockObj = null;
    [SerializeField] private ScoreIndicator _ScoreIndicator = null;

    public string pLetter { get; private set; }
    public TileType pType { get; private set; }
    public Vector2Int pGridPosition { get; private set; }
    public bool pIsLocked { get; private set; }

    private void Awake()
    {
        _Highlight.SetActive(false);
        _BonusObj.SetActive(false);
        _BlockObj.SetActive(false);
        _Locked.SetActive(false);
        pIsLocked = false;
    }

    public void Initialize(string letter, TileType type, Vector2Int pos)
    {
        pLetter = letter; pType = type; pGridPosition = pos;
        _LetterText.text = letter;
        _ScoreIndicator.RefreshDots();
        switch (type)
        {
            case TileType.Bonus:
                _BonusObj.SetActive(true);
                _BlockObj.SetActive(false);
                break;
            case TileType.Blocked:
                _BonusObj.SetActive(false);
                _BlockObj.SetActive(true);
                break;
            default:
                _BonusObj.SetActive(false);
                _BlockObj.SetActive(false);
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
        _Highlight.SetActive(true);
    }

    public void Deselect()
    {
        _Highlight.SetActive(false);
    }

    public void Locked()
    {
        pIsLocked = true;
        _Locked.SetActive(true);
    }

    public void UnblockTile()
    {
        pType = TileType.Normal;
        _BlockObj.GetComponent<Image>().enabled = false;
        _BlockObj.transform.GetChild(0).GetComponent<Animator>().gameObject.SetActive(true);
    }

    public void Consume()
    {
        if (pType != TileType.Blocked)
            gameObject.SetActive(false);
    }

    public bool IsAdjacentTo(TileController other)
    {
        bool adjLinear = false, adjDiagonal = false;
        if (Mathf.Abs(pGridPosition.x - other.pGridPosition.x) + Mathf.Abs(pGridPosition.y - other.pGridPosition.y) == 1)
            adjLinear = true;
        else if (Mathf.Abs(pGridPosition.x - other.pGridPosition.x) == 1 && Mathf.Abs(pGridPosition.y - other.pGridPosition.y) == 1)
            adjDiagonal = true;
        return adjLinear || adjDiagonal;
    }
}
