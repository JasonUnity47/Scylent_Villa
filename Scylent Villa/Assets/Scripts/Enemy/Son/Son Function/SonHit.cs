using UnityEngine;

public class SonHit : MonoBehaviour
{
    // Declaration
    // Script Reference
    private SonFOV sonFOV;

    // Hit
    [Header("Hit")]
    public bool hitPlayer = false;

    private void Start()
    {
        sonFOV = GetComponentInChildren<SonFOV>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && sonFOV.isDetected)
        {
            collision.gameObject.GetComponent<PlayerHealth>().isDead = true;

            hitPlayer = true;
        }
    }
}