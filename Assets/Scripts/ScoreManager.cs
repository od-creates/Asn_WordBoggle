using UnityEngine;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private int totalScore;
    private int wordCount;
    void Awake()
    {
        if (Instance != null)
        {
            if (Instance != this)
                Destroy(this);
        }
        else
            Instance = this;
    }

    public void AddScore(int score)
    {
        totalScore += score;
        wordCount++;
        UIManager.Instance.UpdateScore(totalScore, totalScore / wordCount);
    }

    public void ResetScore()
    {
        totalScore = 0;
        wordCount = 0;
        UIManager.Instance.UpdateScore(0, 0);
    }
}