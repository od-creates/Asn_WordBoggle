using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI avgText;
    public TimerController timer;
    void Awake() => Instance = this;
    public void UpdateScore(int total, int avg)
    {
        scoreText.text = total.ToString();
        avgText.text = avg.ToString();
    }
    public void UpdateTime(int seconds)
    {
        if (timer != null) timer.SetTime(seconds);
    }
}