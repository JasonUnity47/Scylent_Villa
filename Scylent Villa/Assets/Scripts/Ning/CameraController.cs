using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Reference to the player GameObject
    public PlayerMovement playerMovement; // Reference to the PlayerMovement script
    private CinemachineVirtualCamera cinemachineCamera; // Reference to the Cinemachine 2D Camera

    [Header("Camera Offset Settings")]
    public float offsetAmount = 2f; // Adjust this value as needed for the desired offset

    void Start()
    {
        // Get the Cinemachine Virtual Camera component
        cinemachineCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        // Calculate the offset based on the player's movement direction
        Vector3 offset = playerMovement.movement.normalized * offsetAmount;

        // Update the Cinemachine camera's `Tracked Object Offset` based on the calculated offset
        cinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = offset;
    }
}