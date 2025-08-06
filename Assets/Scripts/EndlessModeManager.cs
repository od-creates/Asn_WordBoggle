using UnityEngine;
public class EndlessModeManager : MonoBehaviour
{
    public void Init()
    {
        // generate random grid or pick first raw level
        var levels = JSONLoader.LoadLevels();
        if (levels.Length > 0)
            UIManager.Instance.GetBoardContainer().BuildGrid(levels[0]);
    }
    public void OnWordFound(string word)
    {
        //
    }
}
