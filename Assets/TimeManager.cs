using System;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    bool isSent = false;

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
        PauseTime(true);
        // if (autoStart)
        //     StartTimer();
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
                if (Application.platform == RuntimePlatform.Android && !isSent)
                {
                    using (AndroidJavaClass jc = new AndroidJavaClass("com.azesmwayreactnativeunity.ReactNativeUnityViewManager"))
                    {
                        string jsonString = "{\"score\":" + ScoreManager.Score + ",\"glissNodes\":" + ScoreManager.TotalGlissNodes + "}";
                        jc.CallStatic("sendMessageToMobileApp", jsonString);
                        Debug.Log("sendMessageToMobileApp " + ScoreManager.Score);
                        isSent = true;
                    }
                }
                RestartScene();
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
    }
    public void RestartScene()
    {
        Debug.Log("Restarting Scene...");
        // Get the current scene's build index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        ScoreManager.Score = 0;
        // Reload the current scene
        SceneManager.LoadScene(currentSceneIndex);
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
    public void PauseTime(bool _isTrue)
    {
        Debug.Log("PauseTime : " + _isTrue);
        if (_isTrue)
        {
            Time.timeScale = 0;
            PauseTimer();
        }
        else
        {
            Time.timeScale = 1;
            StartTimer();
        }
    }
}
