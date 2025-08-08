using UnityEngine;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private float mTotalScore;
    private float mTotalWordCount;
    private float mLevelScore;
    private float mLevelWordCount;//level-wise word count(resets each level)

    private void Awake()
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
        mTotalScore += score;
        mLevelScore += score;
        mTotalWordCount++;
        mLevelWordCount++;
        UpdateTotalScoreAndWordCount(mTotalScore, mTotalWordCount);
    }

    public void UpdateTotalScoreAndWordCount(float score, float wordCount)
    {
        mTotalScore = score;
        mTotalWordCount = wordCount;
        UIManager.Instance.UpdateScore(score, wordCount == 0 ? wordCount : (float)(score / wordCount));
    }

    public float GetTotalScore()
    {
        return mTotalScore;
    }

    public float GetTotalWordCount()
    {
        return mTotalWordCount;
    }

    public float GetLevelScore()
    {
        return mLevelScore;
    }

    public float GetLevelWordCount()
    {
        return mLevelWordCount;
    }

    public void ResetLevelWordCountAndScore()
    {
        mLevelWordCount = 0;
        mLevelScore = 0;
    }

    public void ResetTotalWordCountAndScore()
    {
        mTotalScore = 0;
        mTotalWordCount = 0;
        UIManager.Instance.UpdateScore(0, 0);
    }
}