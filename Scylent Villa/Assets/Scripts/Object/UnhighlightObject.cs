using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnhighlightObject : MonoBehaviour
{
    public GameObject unhighlightObject;
    private PlayerHealth playerHealth;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        }
    }

    private void Update()
    {
        if (playerHealth != null)
        {
            if (playerHealth.isDead && unhighlightObject.activeSelf)
            {
                unhighlightObject.SetActive(false);
            }
        }
    }
}
