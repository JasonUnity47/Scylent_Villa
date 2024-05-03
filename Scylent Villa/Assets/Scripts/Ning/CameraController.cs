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
    public float lerpSpeed = 5f; // Speed of interpolation; increase for faster response

    // Current offset of the camera
    private Vector3 currentOffset;

    void Start()
    {
        // Get the Cinemachine Virtual Camera component
        cinemachineCamera = GetComponent<CinemachineVirtualCamera>();

        // Initialize current offset to the camera's current `Tracked Object Offset`
        currentOffset = cinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset;
    }

    void Update()
    {
        // Calculate the desired offset based on the player's movement direction
        Vector3 desiredOffset = playerMovement.movement.normalized * offsetAmount;

        // Smoothly interpolate the current offset towards the desired offset
        currentOffset = Vector3.Lerp(currentOffset, desiredOffset, lerpSpeed * Time.deltaTime);

        // Update the Cinemachine camera's `Tracked Object Offset` based on the current offset
        cinemachineCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = currentOffset;
    }
}