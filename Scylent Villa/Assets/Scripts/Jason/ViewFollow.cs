using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewFollow : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    private void Update()
    {
        if (playerMovement.direction != Vector2.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(playerMovement.direction, transform.TransformDirection(Vector3.forward));

            transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        }
    }
}
