using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ValidWordsPanel : MonoBehaviour
{
    public GameObject validWordLayoutPrefab = null;
    public Transform content = null;
    public TextMeshProUGUI wordCount = null;

    private List<string> selectedValidWordList = new List<string>();

    public void AddValidWord(string validWord)
    {
        selectedValidWordList.Add(validWord);
        var validWordObj = Instantiate(validWordLayoutPrefab, content);
        validWordObj.GetComponent<TextMeshProUGUI>().text = validWord;
        UpdateWordCount();
    }

    public void ClearPanel()
    {
        for (int i = 0; i < content.childCount; i++)
            Destroy(content.GetChild(i).gameObject);
        UpdateWordCount();
        selectedValidWordList.Clear();
    }

    public bool IsUnusedValidWord(string word)
    {
        return !selectedValidWordList.Contains(word);
    }

    private void UpdateWordCount()
    {
        wordCount.text = ScoreManager.Instance.GetLevelWordCount().ToString();
    }
}
