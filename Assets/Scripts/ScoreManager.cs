using UnityEngine;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private int totalScore;
    private int wordCount;
    void Awake() => Instance = this;
    public void AddScore(int score)
    {
        totalScore += score;
        wordCount++;
        UIManager.Instance.UpdateScore(totalScore, totalScore / wordCount);
    }
}