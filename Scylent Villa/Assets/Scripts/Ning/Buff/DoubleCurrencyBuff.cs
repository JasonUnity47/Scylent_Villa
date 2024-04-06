using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleCurrencyBuff : MonoBehaviour
{
    [SerializeField] private float animationDuration = 1.2f; // Duration of the animation
    [SerializeField] private float animationHeight = 0.15f; // Height to move the food prefab
    private BuffSpawner buffSpawner;
    private bool isAnimating = true;
    private int spawnPointIndex; // New variable to store spawn point index

    private void Awake()
    {
        buffSpawner = FindObjectOfType<BuffSpawner>();
        StartCoroutine(Animate());
    }

    // Set spawn point index when instantiated
    public void SetSpawnPointIndex(int index)
    {
        spawnPointIndex = index;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isAnimating = false;
            buffSpawner.DecrementBuffCount();
            buffSpawner.UnlockSpawnPoint(spawnPointIndex); // Unlock spawn point
            Destroy(gameObject);
        }
    }


    private IEnumerator Animate()
    {
        while (isAnimating) // Loop animation if isAnimating is true
        {
            Vector2 originalPos = transform.position;
            Vector2 targetPos = originalPos + new Vector2(0, animationHeight);

            float timeElapsed = 0f;

            while (timeElapsed < animationDuration)
            {
                transform.position = Vector2.Lerp(originalPos, targetPos, timeElapsed / animationDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPos;

            yield return new WaitForSeconds(0f); // Wait for a short time at the top position

            timeElapsed = 0f;

            while (timeElapsed < animationDuration)
            {
                transform.position = Vector2.Lerp(targetPos, originalPos, timeElapsed / animationDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = originalPos;

            yield return null; // Add a small delay before starting the animation again
        }
    }
}
