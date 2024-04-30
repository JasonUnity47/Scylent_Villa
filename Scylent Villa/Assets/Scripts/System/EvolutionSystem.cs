using Pathfinding;
using System.Collections;
using System.Collections.Generic;
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
    public float fovRangePlus;

    // Colors
    [Header("Color")]
    private Color[] colors;
    private int colorIndex;
    private Color pink = new Color(1f, 0.8f, 0.8f);
    private Color orange = new Color(1f, 0.71f, 0.258f);
    private Color red = new Color(1f, 0.239f, 0.239f);
    private Color purple = new Color(0.847f, 0.301f, 1f);
    private Color white = Color.white;
    private int colorCount = 0;

    private void Awake()
    {
        // Set the evolution colors at the beginning stage of the game.
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

        // Initialize the color index to 0 because array indices start at 0.
        colorIndex = 0;
    }

    private void Update()
    {
        // Evolution time is running every frame.
        TimeGone();

        // Keep checking each enemy during the game.
        CheckEnemy();

        // Enemy keep evolving over time.
        EnemyEvolve();
    }

    void TimeGone()
    {
        // The time in the game will keep increasing over time.
        timeBtwEvolution += Time.deltaTime;
    }

    void CheckEnemy()
    {
        // Time to check enemy.
        if (timeBtwEachCheck <= 0)
        {
            // Reset the timer to continue checking the enemies.
            timeBtwEachCheck = checkTime;

            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                // If an enemy Master is in the game scene then assign the enemy to the list.
                if (enemy.GetComponent<Master>())
                {
                    if (!masters.Contains(enemy))
                    {
                        masters.Add(enemy);
                    }
                }

                // Else if an enemy Maid is in the game scene then assign the enemy to the list.
                else if (enemy.GetComponent<Maid>())
                {
                    if (!maids.Contains(enemy))
                    {
                        maids.Add(enemy);
                    }
                }

                // Else if an enemy Son is in the game scene then assign the enemy to the list.
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

        return;
    }

    void EnemyEvolve()
    {
        // Time to Evolve.
        if (timeBtwEvolution >= timeToEvolve)
        {
            // Reset the timer to continue evolving.
            timeBtwEvolution = 0;

            // Stage 2
            // If the timer reaches a predetermined time, the enemies will evolve to the second stage.
            if (timerSystem.timer >= timeToEvolve * 5f && !stage2)
            {
                stage2 = true;
            }

            // Stage 3
            // If the timer reaches a predetermined time, the enemies will evolve to the last stage.
            if (timerSystem.timer >= timeToEvolve * 10f && !stage3)
            {
                stage3 = true;
            }

            foreach (GameObject master in masters)
            {
                // Increase movespeed.
                if (master.GetComponent<AIPath>())
                {
                    master.GetComponent<AIPath>().maxSpeed += moveSpeedPlus;
                }

                // Change color over time.
                if (master.GetComponent<SpriteRenderer>() && colorCount != 10)
                {
                    if (master.GetComponent<SpriteRenderer>().color != colors[colorIndex])
                    {
                        master.GetComponent<SpriteRenderer>().color = colors[colorIndex];
                    }
                }

                // Increase fov angle.
                if (master.GetComponentInChildren<MasterFOV>() && master.GetComponentInChildren<MasterFOV>().fovAngle < 360)
                {
                    master.GetComponentInChildren<MasterFOV>().fovAngle += fovPlus;
                    master.GetComponentInChildren<MasterFOV>().lightView.pointLightInnerAngle += fovPlus;
                    master.GetComponentInChildren<MasterFOV>().lightView.pointLightOuterAngle += fovPlus;
                }

                // If fov angle reach to maximum then increase fov range.
                if (master.GetComponentInChildren<MasterFOV>() && master.GetComponentInChildren<MasterFOV>().fovAngle == 360)
                {
                    master.GetComponentInChildren<MasterFOV>().range += fovRangePlus;
                    master.GetComponentInChildren<MasterFOV>().lightView.pointLightInnerRadius += fovRangePlus;
                    master.GetComponentInChildren<MasterFOV>().lightView.pointLightOuterRadius += fovRangePlus + 1;
                }
            }

            // Maids
            foreach (GameObject maid in maids)
            {
                // Increase movespeed.
                if (maid.GetComponent<AIPath>())
                {
                    maid.GetComponent<AIPath>().maxSpeed += moveSpeedPlus;
                }

                // Change color over time.
                if (maid.GetComponent<SpriteRenderer>() && colorCount != 10)
                {
                    if (maid.GetComponent<SpriteRenderer>().color != colors[colorIndex])
                    {
                        maid.GetComponent<SpriteRenderer>().color = colors[colorIndex];
                    }
                }

                // Increase fov angle.
                if (maid.GetComponentInChildren<MaidFOV>() && maid.GetComponentInChildren<MaidFOV>().fovAngle < 360)
                {
                    maid.GetComponentInChildren<MaidFOV>().fovAngle += fovPlus;
                    maid.GetComponentInChildren<MaidFOV>().lightView.pointLightInnerAngle += fovPlus;
                    maid.GetComponentInChildren<MaidFOV>().lightView.pointLightOuterAngle += fovPlus;
                }

                // If fov angle reach to maximum then increase fov range.
                if (maid.GetComponentInChildren<MaidFOV>() && maid.GetComponentInChildren<MaidFOV>().fovAngle == 360)
                {
                    maid.GetComponentInChildren<MaidFOV>().range += fovRangePlus;
                    maid.GetComponentInChildren<MaidFOV>().lightView.pointLightInnerRadius += fovRangePlus;
                    maid.GetComponentInChildren<MaidFOV>().lightView.pointLightOuterRadius += fovRangePlus + 1;
                }
            }

            // Sons
            foreach (GameObject son in sons)
            {
                // Increase movespeed.
                if (son.GetComponent<AIPath>())
                {
                    son.GetComponent<AIPath>().maxSpeed += moveSpeedPlus;
                }

                // Change color over time.
                if (son.GetComponent<SpriteRenderer>() && colorCount != 10)
                {
                    if (son.GetComponent<SpriteRenderer>().color != colors[colorIndex])
                    {
                        son.GetComponent<SpriteRenderer>().color = colors[colorIndex];
                    }
                }

                // Increase fov angle.
                if (son.GetComponentInChildren<SonFOV>() && son.GetComponentInChildren<SonFOV>().fovAngle < 360)
                {
                    son.GetComponentInChildren<SonFOV>().fovAngle += fovPlus;
                    son.GetComponentInChildren<SonFOV>().lightView.pointLightInnerAngle += fovPlus;
                    son.GetComponentInChildren<SonFOV>().lightView.pointLightOuterAngle += fovPlus;
                }

                // If fov angle reach to maximum then increase fov range.
                if (son.GetComponentInChildren<SonFOV>() && son.GetComponentInChildren<SonFOV>().fovAngle == 360)
                {
                    son.GetComponentInChildren<SonFOV>().range += fovRangePlus;
                    son.GetComponentInChildren<SonFOV>().lightView.pointLightInnerRadius += fovRangePlus;
                    son.GetComponentInChildren<SonFOV>().lightView.pointLightOuterRadius += fovRangePlus + 1;
                }
            }

            // If the color is not reach to the last color then continue change color.
            if (colorCount != 10)
            {
                colorIndex++;

                if (colorIndex + 1 > colors.Length)
                {
                    colorIndex = 0;
                }
            }

            if (colorCount < 10)
            {
                colorCount++;
            }

            // If the color reach to the last color then no color changes anymore.
            else if (colorCount == 10)
            {
                colorCount = 10;
            }
        }
        return;
    }
}
