using UnityEngine;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private float totalScore;
    private float totalWordCount;
    private float levelWordCount;
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
        totalWordCount++;
        levelWordCount++;
        UIManager.Instance.UpdateScore(totalScore, (float)(totalScore / totalWordCount));
    }

    public float GetTotalScore()
    {
        return totalScore;
    }

    public float GetLevelWordCount()
    {
        return levelWordCount;
    }

    public void ResetLevelWordCount()
    {
        levelWordCount = 0;
    }

    public void ResetScore()
    {
        totalScore = 0;
        totalWordCount = 0;
        UIManager.Instance.UpdateScore(0, 0);
    }
}