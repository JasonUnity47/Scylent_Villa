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
    private TimerSystem timerSystem;
    private float timeBtwEvolution;
    public float timeToEvolve = 30f;

    public float checkTime;
    private float timeBtwEachCheck;

    // Evolve Stage
    [Header("Stage")]
    public bool stage2 = false;
    public bool stage3 = false;

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

    // Colors
    [Header("Color")]
    private Color[] colors;
    private int colorIndex;
    private Color pink = new Color(1f, 0.8f, 0.8f);
    private Color orange = new Color(1f, 0.71f, 0.258f);
    private Color red = new Color(1f, 0.239f, 0.239f);
    private Color purple = new Color(0.847f, 0.301f, 1f);
    private Color white = Color.white;

    private void Awake()
    {
        colors = new Color[] { pink, orange, red, purple, white };
    }

    private void Start()
    {
        // Get TimerSystem
        timerSystem = GetComponent<TimerSystem>();

        // Initialize timer to 0.
        timeBtwEvolution = 0;

        // Initialize time between each check to predefined check time.
        timeBtwEachCheck = checkTime;

        colorIndex = 0;
    }

    private void Update()
    {
        // Time is running every frame.
        TimeGone();

        // Keep checking each enemy during the game.
        CheckEnemy();

        // Enemy keep evolving over time.
        EnemyEvolve();
    }

    void TimeGone()
    {
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
        if (timeBtwEvolution >= timeToEvolve)
        {
            timeBtwEvolution = 0;

            // Masters
            if (timerSystem.timer >= timeToEvolve * 1f && !stage2)
            {
                stage2 = true;
            }
            // Masters
            if (timerSystem.timer >= timeToEvolve * 2f && !stage3)
            {
                stage3 = true;
            }


            foreach (GameObject master in masters)
            {
                if (master.GetComponent<AIPath>())
                {
                    master.GetComponent<AIPath>().maxSpeed += 0.5f;
                }

                if (master.GetComponent<SpriteRenderer>())
                {
                    if (master.GetComponent<SpriteRenderer>().color != colors[colorIndex])
                    {
                        master.GetComponent<SpriteRenderer>().color = colors[colorIndex];
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
                    if (maid.GetComponent<SpriteRenderer>().color != colors[colorIndex])
                    {
                        maid.GetComponent<SpriteRenderer>().color = colors[colorIndex];
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
                    if (son.GetComponent<SpriteRenderer>().color != colors[colorIndex])
                    {
                        son.GetComponent<SpriteRenderer>().color = colors[colorIndex];
                    }
                }

                if (son.GetComponentInChildren<SonFOV>())
                {
                    son.GetComponentInChildren<SonFOV>().fovAngle += 10f;
                    son.GetComponentInChildren<SonFOV>().lightView.pointLightInnerAngle += 10;
                    son.GetComponentInChildren<SonFOV>().lightView.pointLightOuterAngle += 10;
                }
            }

            colorIndex++;

            if (colorIndex + 1 > colors.Length)
            {
                colorIndex = 0;
            }
        }
    }
}
