using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ValidWordsPanel : MonoBehaviour
{
    public GameObject validWordLayoutPrefab = null;
    public Transform content = null;
    public TextMeshProUGUI wordCount = null;

    public void AddValidWord(string validWord)
    {
        var validWordObj = Instantiate(validWordLayoutPrefab, content);
        validWordObj.GetComponent<TextMeshProUGUI>().text = validWord;
        UpdateWordCount();
    }

    public void ClearPanel()
    {
        for (int i = 0; i < content.childCount; i++)
            Destroy(content.GetChild(i).gameObject);
        UpdateWordCount();
    }

    private void UpdateWordCount()
    {
        wordCount.text = ScoreManager.Instance.GetWordCount().ToString();
    }
}
