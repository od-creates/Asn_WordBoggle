using UnityEngine;
using System.Collections.Generic;
public class DictionaryManager : MonoBehaviour
{
    public static DictionaryManager Instance { get; private set; }
    private HashSet<string> mWords;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadWords();
        }
        else Destroy(gameObject);
    }

    private void LoadWords()
    {
        mWords = new HashSet<string>(System.StringComparer.OrdinalIgnoreCase);
        TextAsset txt = Resources.Load<TextAsset>("wordList");
        if (txt == null)
        {
            Debug.LogError("wordList.txt missing in Resources folder");
            return;
        }
        foreach (string w in txt.text.Split('\n'))
        {
            string clean = w.Trim();
            if (!string.IsNullOrEmpty(clean)) mWords.Add(clean);
        }
    }

    public bool IsValid(string word) => mWords.Contains(word);
}
