using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class TimerController : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI timeText;

    private float remainingTime;
    private Coroutine timerRoutine;

    /// <summary>
    /// Raised when the countdown reaches zero.
    /// </summary>
    public event Action OnTimerFinished;

    /// <summary>
    /// Initialize the timer to a given number of seconds (but does not start it).
    /// </summary>
    public void SetTime(int seconds)
    {
        remainingTime = seconds;
        UpdateDisplay();
    }

    /// <summary>
    /// Begins (or restarts) the countdown.
    /// </summary>
    public void StartTimer()
    {
        // Stop any existing coroutine so we don't double-count
        if (timerRoutine != null)
            StopCoroutine(timerRoutine);

        timerRoutine = StartCoroutine(RunTimer());
    }

    /// <summary>
    /// Halts the countdown (e.g. when the level ends early).
    /// </summary>
    public void StopTimer()
    {
        if (timerRoutine != null)
            StopCoroutine(timerRoutine);
    }

    private IEnumerator RunTimer()
    {
        while (remainingTime > 0f)
        {
            yield return new WaitForSeconds(1f);
            remainingTime--;
            UpdateDisplay();
        }

        // Time's up!
        OnTimerFinished?.Invoke();
    }

    /// <summary>
    /// Formats the remainingTime into MM:SS and updates the UI text.
    /// Also notifies UIManager if you want a centralized UI update.
    /// </summary>
    private void UpdateDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        timeText.text = $"{minutes:00}:{seconds:00}";

        // (Optional) sync with UIManager if other UI elements depend on time
        UIManager.Instance?.UpdateTime((int)remainingTime);
    }
}