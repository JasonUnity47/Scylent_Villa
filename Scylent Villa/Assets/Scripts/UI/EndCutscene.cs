using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCutscene : MonoBehaviour
{
    IEnumerator WaitEnd()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(WaitEnd());
    }
}
