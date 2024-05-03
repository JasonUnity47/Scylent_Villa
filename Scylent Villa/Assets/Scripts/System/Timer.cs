using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Declaration
    public float rawTime;

    private float seconds;
    private float minutes;

    public string secondAmount;
    public string minuteAmount;

    private void Start()
    {
        rawTime = 0;
    }

    private void Update()
    {
        // Start counting the time.
        rawTime += Time.deltaTime;

        seconds = Mathf.FloorToInt(rawTime % 60);
        minutes = Mathf.Floor(rawTime / 60);

        if (minutes <= 60)
        {
            minuteAmount = minutes.ToString("00");
        }

        if (seconds <= 60)
        {
            secondAmount = seconds.ToString("00");
        }
    }
}
