using UnityEngine;
public class EndlessModeManager : MonoBehaviour
{
    public void Init()
    {
        var endless = JSONLoader.LoadEndless();
        UIManager.Instance.GetBoardContainer().BuildGrid(endless);
    }
    public void OnWordFound(string word)
    {
        //
    }
}
