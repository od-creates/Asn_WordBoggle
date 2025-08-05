using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameMode mode;
    public int selectedLevelIndex = 0;
    public BoardManager boardManager;
    public LevelManager levelManager;
    public EndlessModeManager endlessManager;

    void Awake()
    {
        Instance = this;
    }

    public void StartGame()
    {
        Reset();
        switch (mode)
        {
            case GameMode.Endless:
                endlessManager.Init();
                break;
            case GameMode.Levels:
                levelManager.Init(selectedLevelIndex);
                break;
        }
    }

    public void Reset()
    {
        boardManager.ClearTiles();
    }
}