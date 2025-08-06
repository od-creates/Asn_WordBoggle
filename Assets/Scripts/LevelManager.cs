using UnityEngine;
public class LevelManager : MonoBehaviour
{
    private LevelDataRaw[] levels;
    public TimerController timer;

    void Awake()
    {
        levels = JSONLoader.LoadLevels();
    }

    /// <summary>
    /// Initialize given level index
    /// </summary>
    public void Init(int levelIndex)
    {
        if (levels == null || levels.Length == 0) return;
        LevelDataRaw lvl = levels[levelIndex % levels.Length];
        UIManager.Instance.GetBoardContainer().BuildGrid(lvl);
        if (timer != null && lvl.timeSec > 0)
        {
            timer.SetTime(lvl.timeSec);
            timer.StartTimer();
        }
    }

    public void OnWordFound(string word)
    {
        // track objectives per level
    }
}