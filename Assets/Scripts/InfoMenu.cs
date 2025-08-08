using UnityEngine;

public class InfoMenu : MonoBehaviour
{
    [SerializeField] private GameObject _EndLessUI;
    [SerializeField] private GameObject _LevelsUI;
    [SerializeField] private GameObject _InfoTooltip = null;

    public void SetEndlessModeUI()
    {
        _EndLessUI.SetActive(true);
        _LevelsUI.SetActive(false);
    }

    public void SetLevelsModeUI()
    {
        _EndLessUI.SetActive(false);
        _LevelsUI.SetActive(true);
    }

    public void OnClickInfoBtn()
    {
        if (_InfoTooltip != null)
        {
            _InfoTooltip.SetActive(!_InfoTooltip.activeInHierarchy);
        }
    }
}
