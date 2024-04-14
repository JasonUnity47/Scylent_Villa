using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    // Declaration
    public float fovAngle = 90f;
    public float range = 8;
    public float radius = 5;
    [SerializeField] private LayerMask whatIsItem;

    [SerializeField] private Collider2D[] items;

    public float checkTime;
    private float timeBtwEachCheck;

    private void Start()
    {
        timeBtwEachCheck = checkTime;
    }

    private void Update()
    {
        if (timeBtwEachCheck <= 0)
        {
            items = Physics2D.OverlapCircleAll(transform.position, radius, whatIsItem);

            if (items.Length != 0)
            {
                foreach (Collider2D i in items)
                {
                    Vector2 direction = i.transform.position - transform.position;
                    float angle = Vector3.Angle(direction, transform.up);

                    RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, range, whatIsItem);

                    if (hits.Length != 0)
                    {
                        foreach (RaycastHit2D hit in hits)
                        {
                            if (angle < fovAngle / 2)
                            {
                                if (hit.collider.CompareTag("Item"))
                                {
                                    Highlight(hit);
                                    Debug.DrawRay(transform.position, direction, Color.red);
                                }
                            }

                            else
                            {
                                Unhighlight(hit);
                            }
                        }
                    }
                }
            }
        }

        else
        {
            timeBtwEachCheck -= Time.deltaTime;
        }
    }

    void Highlight(RaycastHit2D obj)
    {
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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -range));
    }
}
