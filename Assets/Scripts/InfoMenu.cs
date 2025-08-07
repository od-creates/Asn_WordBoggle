using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoMenu : MonoBehaviour
{
    public GameObject endLessUI;
    public GameObject levelsUI;
    public GameObject infoTooltip = null;

    public void SetEndlessModeUI()
    {
        endLessUI.SetActive(true);
        levelsUI.SetActive(false);
    }

    public void SetLevelsModeUI()
    {
        endLessUI.SetActive(false);
        levelsUI.SetActive(true);
    }

    public void OnClickInfoBtn()
    {
        if (infoTooltip != null)
        {
            infoTooltip.SetActive(!infoTooltip.activeInHierarchy);
        }
    }
}
