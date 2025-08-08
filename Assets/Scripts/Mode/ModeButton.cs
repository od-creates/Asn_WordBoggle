using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ModeButton : MonoBehaviour
{
    [SerializeField] private GameMode _ButtonMode;

    private Button mButton;

    private void Awake()
    {
        mButton = GetComponent<Button>();
        mButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        UIManager.Instance.OnStartGame();
        UIManager.Instance.SetInfoMenuUI(_ButtonMode);
        GameManager.Instance.pMode = _ButtonMode;
        GameManager.Instance.StartGame();
    }
}