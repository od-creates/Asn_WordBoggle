using UnityEngine;
public class EndlessModeManager : MonoBehaviour
{
    public void Init()
    {
        GameManager.Instance.SetTimeUp(false);
        var endless = JSONLoader.LoadEndless();
        UIManager.Instance.GetBoardContainer().BuildGrid(endless);
    }
    public void OnWordFound(string word)
    {
        //
    }
}
