using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    // Declaration
    // FOV Variable
    [Header("FOV Variable")]
    public float fovAngle = 90f;
    public float range = 8;
    public float radius = 5;
    [SerializeField] private LayerMask whatIsItem;

    // Collider
    [Header("Collider")]
    [SerializeField] private Collider2D[] items;

    // Timer
    [Header("Check Timer")]
    public float checkTime;
    private float timeBtwEachCheck;

    private void Start()
    {
        // Initialize the timer.
        timeBtwEachCheck = checkTime;
    }

    private void Update()
    {
        // If the timer reach to 0.
        if (timeBtwEachCheck <= 0)
        {
            // Check whether an item is around the player.
            items = Physics2D.OverlapCircleAll(transform.position, radius, whatIsItem);

            // If an item is around the player.
            if (items.Length != 0)
            {
                // Check each item the player detected.
                foreach (Collider2D i in items)
                {
                    Vector2 direction = i.transform.position - transform.position;
                    float angle = Vector3.Angle(direction, transform.up);

                    // Check whether the player is looking at the item(s).
                    RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, range, whatIsItem);

                    // If an item is in the line of sight of the player.
                    if (hits.Length != 0)
                    {
                        // Check each item the player can see.
                        foreach (RaycastHit2D hit in hits)
                        {
                            // If the item/object is in the sight of the player.
                            if (angle < fovAngle / 2)
                            {
                                // If the collider is an item or buff.
                                if (hit.collider.CompareTag("Item") || hit.collider.CompareTag("AccelerationBuff") || hit.collider.CompareTag("DoubleCurrencyBuff") || hit.collider.CompareTag("IncreaseFOVBuff"))
                                {
                                    // Highlight the object.
                                    Highlight(hit);

                                    // Draw a line to show the sight of the player.
                                    Debug.DrawRay(transform.position, direction, Color.red);
                                }
                            }

                            else
                            {
                                // Unhighlight the object.
                                Unhighlight(hit);
                            }
                        }
                    }
                }
            }
        }

        else
        {
            // The timer is continue decreasing over time.
            timeBtwEachCheck -= Time.deltaTime;
        }
    }

    void Highlight(RaycastHit2D obj)
    {
        // Highlight the object.
        if (obj.transform.childCount != 0 && obj.transform.Find("Outline") == true)
        {
            obj.transform.GetChild(0).gameObject.SetActive(true);
        }

        if (obj.transform.childCount > 1 && obj.transform.Find("Increase Arrow"))
        {
            obj.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        }
    }

    void Unhighlight(RaycastHit2D obj)
    {
        // Unhighlight the object.
        if (obj.transform.childCount != 0 && obj.transform.Find("Outline") == true)
        {
            obj.transform.GetChild(0).gameObject.SetActive(false);
        }

        if (obj.transform.childCount > 1 && obj.transform.Find("Increase Arrow"))
        {
            obj.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw lines to aid us to visualise the detection area.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -range));
    }
}
