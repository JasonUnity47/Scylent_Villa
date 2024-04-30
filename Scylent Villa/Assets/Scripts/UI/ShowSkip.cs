using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSkip : MonoBehaviour
{
    public GameObject skipButton;

    private void Start()
    {
        StartCoroutine(WaitSkip());
    }

    IEnumerator WaitSkip()
    {
        yield return new WaitForSeconds(3f);

        if (!skipButton.activeSelf)
        {
            skipButton.SetActive(true);
        }
    }
}
