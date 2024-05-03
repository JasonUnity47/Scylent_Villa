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

        // Calculate minutes and seconds separately
        minutes = Mathf.FloorToInt(rawTime / 60);
        seconds = Mathf.FloorToInt(rawTime % 60);

        // Format the time to display with leading zeros
        minuteAmount = minutes.ToString("00");
        secondAmount = seconds.ToString("00");
    }
}
