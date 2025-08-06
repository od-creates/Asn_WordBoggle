using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ValidWordsPanel : MonoBehaviour
{
    public GameObject validWordLayoutPrefab = null;
    public Transform layout = null;

    public void AddValidWord(string validWord)
    {
        var validWordObj = Instantiate(validWordLayoutPrefab, layout);
        validWordObj.GetComponent<TextMeshProUGUI>().text = validWord;
    }

    public void ClearPanel()
    {
        for (int i = 0; i < layout.childCount; i++)
            Destroy(layout.GetChild(i).gameObject);
    }
}
