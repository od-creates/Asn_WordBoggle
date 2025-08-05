using UnityEngine;
using System.Collections.Generic;
public class DictionaryManager : MonoBehaviour
{
    public static DictionaryManager Instance { get; private set; }
    private HashSet<string> words;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadWords();
        }
        else Destroy(gameObject);
    }

    void LoadWords()
    {
        words = new HashSet<string>(System.StringComparer.OrdinalIgnoreCase);
        TextAsset txt = Resources.Load<TextAsset>("wordList");
        if (txt == null)
        {
            Debug.LogError("wordList.txt missing in Resources folder");
            return;
        }
        foreach (string w in txt.text.Split('\n'))
        {
            string clean = w.Trim();
            if (!string.IsNullOrEmpty(clean)) words.Add(clean);
        }
    }

    public bool IsValid(string word) => words.Contains(word);
}
