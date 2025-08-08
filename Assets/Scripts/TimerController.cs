using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class TimerController : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI _TimeText;
    [SerializeField] private Color _NormalTimerColor = Color.green;
    [SerializeField] private Color _WarningTimerColor = Color.red;

    private float mRemainingTime, mTotalTime;
    private Coroutine mTimerRoutine;
    
    public event Action pOnTimerFinished;

    private IEnumerator RunTimer()
    {
        while (mRemainingTime > 0f)
        {
            yield return new WaitForSeconds(1f);
            mRemainingTime--;
            UpdateTimerDisplay();
        }

        //Time exhausted
        pOnTimerFinished?.Invoke();
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(mRemainingTime / 60f);
        int seconds = Mathf.FloorToInt(mRemainingTime % 60f);
        _TimeText.text = $"{minutes:00}:{seconds:00}";
        _TimeText.color = seconds <= (int)(0.3 * mTotalTime) ? _WarningTimerColor : _NormalTimerColor;
    }

    /// <summary>
    /// Initialize the timer to a given number of seconds (but does not start it).
    /// </summary>
    public void SetTime(int seconds)
    {
        mTotalTime = seconds;
        mRemainingTime = seconds;
        UpdateTimerDisplay();
    }

    /// <summary>
    /// Begins (or restarts) the countdown.
    /// </summary>
    public void StartTimer()
    {
        if (mTimerRoutine != null)
            StopCoroutine(mTimerRoutine);

        mTimerRoutine = StartCoroutine(RunTimer());
    }

    /// <summary>
    /// Halts the countdown
    /// </summary>
    public void StopTimer()
    {
        if (mTimerRoutine != null)
            StopCoroutine(mTimerRoutine);
    }

    
}