using UnityEngine;

public class MaidHit : MonoBehaviour
{
    // Declaration
    // Script Reference
    private MaidFOV maidFOV;

    // Hit
    [Header("Hit")]
    public bool hitPlayer = false;

    private void Start()
    {
        // Get reference.
        maidFOV = GetComponentInChildren<MaidFOV>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && maidFOV.isDetected)
        {
            collision.gameObject.GetComponent<PlayerHealth>().isDead = true;

            hitPlayer = true;
        }
    }
}