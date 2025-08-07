using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class ModeButton : MonoBehaviour
{
    [Tooltip("Which game mode this button selects.")]
    public GameMode buttonMode;

    private Button _button;

    void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        UIManager.Instance.OnStartGame();
        UIManager.Instance.SetInfoMenuUI(buttonMode);
        GameManager.Instance.mode = buttonMode;
        GameManager.Instance.StartGame();
    }
}