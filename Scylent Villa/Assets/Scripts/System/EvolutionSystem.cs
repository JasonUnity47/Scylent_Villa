using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EvolutionSystem : MonoBehaviour
{
    // Declaration
    // Timer
    [Header("TIMER")]
    public float evolutionTimer;
    private float timeBtwEvolution;

    public float checkTime;
    private float timeBtwEachCheck;

    // Enemies
    [Header("ENEMIES")]
    private GameObject[] enemies;
    public List<GameObject> masters;
    public List<GameObject> maids;
    public List<GameObject> sons;

    // Increment
    [Header("Increment")]
    public float moveSpeedPlus;
    public float fovPlus;

    private void Start()
    {
        // Initialize timer to 0.
        evolutionTimer = 0;
        timeBtwEvolution = 0;

        // Initialize time between each check to predefined check time.
        timeBtwEachCheck = checkTime;
    }

    private void Update()
    {
        // Time is running every frame.
        TimeGone();

        // Keep checking each enemy during the game.
        CheckEnemy();

        EnemyEvolve();
    }

    void TimeGone()
    {
        evolutionTimer += Time.deltaTime;

        timeBtwEvolution += Time.deltaTime;
    }

    void CheckEnemy()
    {
        if (timeBtwEachCheck <= 0)
        {
            timeBtwEachCheck = checkTime;

            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                if (enemy.GetComponent<Master>())
                {
                    if (!masters.Contains(enemy))
                    {
                        masters.Add(enemy);
                    }
                }

                else if (enemy.GetComponent<Maid>())
                {
                    if (!maids.Contains(enemy))
                    {
                        maids.Add(enemy);
                    }
                }

                else if (enemy.GetComponent<Son>())
                {
                    if (!sons.Contains(enemy))
                    {
                        sons.Add(enemy);
                    }
                }
            }
        }

        else
        {
            timeBtwEachCheck -= Time.deltaTime;
        }
    }

    void EnemyEvolve()
    {
        if (timeBtwEvolution >= 15f)
        {
            timeBtwEvolution = 0;

            // Masters
            foreach (GameObject master in masters)
            {
                if (master.GetComponent<AIPath>())
                {
                    master.GetComponent<AIPath>().maxSpeed += 0.5f;
                }

                if (master.GetComponent<SpriteRenderer>())
                {
                    if (master.GetComponent<SpriteRenderer>().color != Color.red)
                    {
                        master.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                }

                if (master.GetComponentInChildren<MasterFOV>())
                {
                    master.GetComponentInChildren<MasterFOV>().fovAngle += 10f;
                    master.GetComponentInChildren<MasterFOV>().lightView.pointLightInnerAngle += 10;
                    master.GetComponentInChildren<MasterFOV>().lightView.pointLightOuterAngle += 10;
                }
            }

            // Maids
            foreach (GameObject maid in maids)
            {
                if (maid.GetComponent<AIPath>())
                {
                    maid.GetComponent<AIPath>().maxSpeed += 0.5f;
                }

                if (maid.GetComponent<SpriteRenderer>())
                {
                    if (maid.GetComponent<SpriteRenderer>().color != Color.green)
                    {
                        maid.GetComponent<SpriteRenderer>().color = Color.green;
                    }
                }

                if (maid.GetComponentInChildren<MaidFOV>())
                {
                    maid.GetComponentInChildren<MaidFOV>().fovAngle += 10f;
                    maid.GetComponentInChildren<MaidFOV>().lightView.pointLightInnerAngle += 10;
                    maid.GetComponentInChildren<MaidFOV>().lightView.pointLightOuterAngle += 10;
                }
            }

            // Sons
            foreach (GameObject son in sons)
            {
                if (son.GetComponent<AIPath>())
                {
                    son.GetComponent<AIPath>().maxSpeed += 0.5f;
                }

                if (son.GetComponent<SpriteRenderer>())
                {
                    if (son.GetComponent<SpriteRenderer>().color != Color.blue)
                    {
                        son.GetComponent<SpriteRenderer>().color = Color.blue;
                    }
                }

                if (son.GetComponentInChildren<SonFOV>())
                {
                    son.GetComponentInChildren<SonFOV>().fovAngle += 10f;
                    son.GetComponentInChildren<SonFOV>().lightView.pointLightInnerAngle += 10;
                    son.GetComponentInChildren<SonFOV>().lightView.pointLightOuterAngle += 10;
                }
            }
        }
    }
}
