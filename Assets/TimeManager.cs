using System;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public float totalTime = 60.0f;   // Total time for the timer.
    public bool autoStart = true;     // Whether the timer should start automatically.
    public bool loop = false;         // Whether the timer should restart after reaching zero.

    public UnityEvent OnTimerStart;   // Event invoked when the timer starts.
    public UnityEvent OnTimerTick;    // Event invoked every second.
    public UnityEvent OnTimerEnd;     // Event invoked when the timer reaches zero.

    public TextMeshProUGUI timerText; // Reference to your UI Text element.
    public bool gameEnded;
    private float currentTime;
    public bool isRunning = false;
    public static TimeManager instance;
  //  UiManager myUiManager;
  //  FinishManager myFinishManager;
    ScoreManager myScoreManager;

    private void Awake()
    {
        // Check if an instance already exists.
        if (instance == null)
        {
            // If not, set the current instance to this.
            instance = this;
        }
        else
        {
            // If an instance already exists, destroy this new instance.
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        // myUiManager = UiManager.instance;
      //  myScoreManager = ScoreManager.instance;
        //   myFinishManager = FinishManager.instance;

        if (autoStart)
            StartTimer();
    }

    private void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime;
            OnTimerTick.Invoke();

            if (currentTime <= 0)
            {
                currentTime = 0;
                OnTimerEnd.Invoke();

                if (loop)
                    StartTimer();
                else
                    isRunning = false;
            }
            if (currentTime == 0)
            {
                //myScoreManager.EndGame(false);
                Time.timeScale = 0;
                // myFinishManager.ShowButton();
                // myUiManager.ShowLosePanel();
                // if (Application.platform == RuntimePlatform.Android)
                // {
                //     using (AndroidJavaClass jc = new AndroidJavaClass("com.azesmwayreactnativeunity.ReactNativeUnityViewManager"))
                //     {
                //         jc.CallStatic("sendMessageToMobileApp", myScoreManager.score);
                //         Debug.Log("sendMessageToMobileApp " + myScoreManager.score);
                //     }
                // }

            }
            // Update the UI Text to display the remaining time.
            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
            string formattedTime = string.Format("{0:D2}", timeSpan.Seconds);
            timerText.text = formattedTime;
        }
    }

    public void StartTimer()
    {
        currentTime = totalTime;
        isRunning = true;
        OnTimerStart.Invoke();
        // myFinishManager.HideButton();
    }

    public void PauseTimer()
    {
        isRunning = false;
    }

    public void ResumeTimer()
    {
        isRunning = true;
    }

    public void ResetTimer()
    {
        currentTime = totalTime;
        isRunning = false;
    }
}