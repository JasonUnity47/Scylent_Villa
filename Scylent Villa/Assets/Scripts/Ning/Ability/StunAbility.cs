using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunAbility : MonoBehaviour
{
    public float stunRadius = 2f; // Radius within which the player can stun enemies
    

    public LayerMask enemyLayer; // Layer mask for detecting enemies

    public bool CanUseStunAbility()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, stunRadius, enemyLayer);

        foreach (Collider2D enemyCollider in hitEnemies)
        {
            Son sonEnemy = enemyCollider.GetComponent<Son>();
            Maid maidEnemy = enemyCollider.GetComponent<Maid>();
            Master masterEnemy = enemyCollider.GetComponent<Master>();

            if ((sonEnemy != null && !sonEnemy.sonFOV.IsPlayerDetected()) ||
                (maidEnemy != null && !maidEnemy.maidFOV.IsPlayerDetected()) ||
                (masterEnemy != null && !masterEnemy.masterFOV.IsPlayerDetected()))
            {
                return true; // Can use stun ability if an enemy is inside stun radius and player is not detected
            }
        }

        return false; // Cannot use stun ability otherwise
    }

    public void UseStunAbility(float stunDuration)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, stunRadius, enemyLayer);

        foreach (Collider2D enemyCollider in hitEnemies)
        {
            Son sonEnemy = enemyCollider.GetComponent<Son>();
            Maid maidEnemy = enemyCollider.GetComponent<Maid>();
            Master masterEnemy = enemyCollider.GetComponent<Master>();

            if (sonEnemy != null && !sonEnemy.sonFOV.IsPlayerDetected())
            {
                // Stun the enemy
                sonEnemy.Stun(stunDuration);
            }

            if (maidEnemy != null && !maidEnemy.maidFOV.IsPlayerDetected())
            {
                // Stun the enemy
                maidEnemy.Stun(stunDuration);
            }

            if (masterEnemy != null && !masterEnemy.masterFOV.IsPlayerDetected())
            {
                // Stun the enemy
                masterEnemy.Stun(stunDuration);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stunRadius);
    }
}
