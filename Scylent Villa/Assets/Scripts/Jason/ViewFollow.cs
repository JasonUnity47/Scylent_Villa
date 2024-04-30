using UnityEngine;

public class ViewFollow : MonoBehaviour
{
    // Declaration
    // Script Reference
    [Header("Script Reference")]
    [SerializeField] private PlayerMovement playerMovement;

    private void Update()
    {
        // If the facing direction is not null.
        if (playerMovement.direction != Vector2.zero)
        {
            // Rotate the FOV.
            Quaternion rotation = Quaternion.LookRotation(playerMovement.direction, transform.TransformDirection(Vector3.forward));

            transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        }
    }
}