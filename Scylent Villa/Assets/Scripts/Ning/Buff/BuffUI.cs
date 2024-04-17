using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BuffUI : MonoBehaviour
{
    public GameObject accelerationUI;
    public GameObject increaseFOVUI;
    public GameObject doubleCurrencyUI;
    public TextMeshProUGUI accelerationText;
    public TextMeshProUGUI increaseFOVText;
    public TextMeshProUGUI doubleCurrencyText;

    private Coroutine accelerationBuffCoroutine;
    private Coroutine increaseFOVBuffCoroutine;
    private Coroutine doubleCurrencyBuffCoroutine;

    public void ShowAccelerationBuffUI(float duration)
    {
        // Show the buff panel and set the buff text
        accelerationUI.SetActive(true);

        // If there's already a countdown coroutine running, stop it
        if (accelerationBuffCoroutine != null)
            StopCoroutine(accelerationBuffCoroutine);

        // Start a new coroutine to countdown the duration of the buff
        accelerationBuffCoroutine = StartCoroutine(AccelerationBuffTimer(duration));
    }

    IEnumerator AccelerationBuffTimer(float duration)
    {
        float accelerationRemainingTime = duration;

        while (accelerationRemainingTime > 0)
        {
            // Update countdown text
            accelerationText.text = accelerationRemainingTime.ToString();

            // Wait for 1 second
            yield return new WaitForSeconds(1f);

            // Decrease remaining time
            accelerationRemainingTime -= 1f;
        }

        // Hide the buff panel when the duration is over
        accelerationUI.SetActive(false);
    }

    public void ShowIncreaseFOVBuffUI(float duration)
    {
        // Show the buff panel and set the buff text
        increaseFOVUI.SetActive(true);
       
        // If there's already a countdown coroutine running, stop it
        if (increaseFOVBuffCoroutine != null)
            StopCoroutine(increaseFOVBuffCoroutine);

        // Start a new coroutine to countdown the duration of the buff
        increaseFOVBuffCoroutine = StartCoroutine(IncreaseFOVBuffTimer(duration));
    }

    IEnumerator IncreaseFOVBuffTimer(float duration)
    {
        float increaseFOVRemainingTime = duration;

        while (increaseFOVRemainingTime > 0)
        {
            // Update countdown text
            increaseFOVText.text = increaseFOVRemainingTime.ToString();

            // Wait for 1 second
            yield return new WaitForSeconds(1f);

            // Decrease remaining time
            increaseFOVRemainingTime -= 1f;
        }

        // Hide the buff panel when the duration is over
        increaseFOVUI.SetActive(false);
    }

    public void ShowDoubleCurrencyBuffUI(float duration)
    {
        // Show the buff panel and set the buff text
        doubleCurrencyUI.SetActive(true);
        doubleCurrencyText.text = duration.ToString();


        // If there's already a countdown coroutine running, stop it
        if (doubleCurrencyBuffCoroutine != null)
            StopCoroutine(doubleCurrencyBuffCoroutine);

        // Start a new coroutine to countdown the duration of the buff
        doubleCurrencyBuffCoroutine = StartCoroutine(DoubleCurrencyBuffTimer(duration));
    }

    IEnumerator DoubleCurrencyBuffTimer(float duration)
    {
        float doubleCurrencyRemainingTime = duration;

        while (doubleCurrencyRemainingTime > 0)
        {
            // Update countdown text
            doubleCurrencyText.text = doubleCurrencyRemainingTime.ToString();

            // Wait for 1 second
            yield return new WaitForSeconds(1f);

            // Decrease remaining time
            doubleCurrencyRemainingTime -= 1f;
        }

        // Hide the buff panel when the duration is over
        doubleCurrencyUI.SetActive(false);
    }
}