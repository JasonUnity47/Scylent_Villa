using UnityEngine;

public class MasterHit : MonoBehaviour
{
    // Declaration
    // Script Reference
    private MasterFOV masterFOV;

    // Hit
    [Header("Hit")]
    public bool hitPlayer = false;

    private void Start()
    {
        // Get reference.
        masterFOV = GetComponentInChildren<MasterFOV>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && masterFOV.isDetected)
        {
            collision.gameObject.GetComponent<PlayerHealth>().isDead = true;

            hitPlayer = true;
        }
    }
}