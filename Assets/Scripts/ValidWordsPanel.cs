using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ValidWordsPanel : MonoBehaviour
{
    [SerializeField] private GameObject _ValidWordLayoutPrefab = null;
    [SerializeField] private Transform _Content = null;
    [SerializeField] private TextMeshProUGUI _WordCount = null;

    private List<string> mSelectedValidWordList = new List<string>();

    private void UpdateWordCount()
    {
        _WordCount.text = ScoreManager.Instance.GetLevelWordCount().ToString();
    }

    public void AddValidWord(string validWord)
    {
        mSelectedValidWordList.Add(validWord);
        var validWordObj = Instantiate(_ValidWordLayoutPrefab, _Content);
        validWordObj.GetComponent<TextMeshProUGUI>().text = validWord;
        UpdateWordCount();
    }

    public void ClearPanel()
    {
        for (int i = 0; i < _Content.childCount; i++)
            Destroy(_Content.GetChild(i).gameObject);
        UpdateWordCount();
        mSelectedValidWordList.Clear();
    }

    public bool IsUnusedValidWord(string word)
    {
        return !mSelectedValidWordList.Contains(word);
    }

    
}
