using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToolRelease : MonoBehaviour
{
    // Initial spawn position and rotation
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    // Respawn delay time
    public float respawnDelay = 3.0f;

    private XRGrabInteractable grabInteractable;

    public bool isHeld = false;

    public string type;

    void Start()
    {
        // Store the initial position and rotation of the tool
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        grabInteractable = GetComponent<XRGrabInteractable>();

        // Subscribe to the "Select Exited" event (when the tool is released)
        grabInteractable.selectExited.AddListener(OnRelease);
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    // Called when the tool is grabbed
    private void OnGrab(SelectEnterEventArgs args)
    {
        isHeld = true;
        // When the tool is grabbed, rotate it to face same way the player is facing
        if (type == "blowdryer")
        {
            // Rotate the tool to face the opposite direction of the player
            transform.rotation = Quaternion.LookRotation(-args.interactor.transform.forward, Vector3.up);
        }
    }

    // Called when the tool is released
    private void OnRelease(SelectExitEventArgs args)
    {
        isHeld = false;  
        StartCoroutine(RespawnTool());  
    }

    // Respawn the tool after a delay
    private IEnumerator RespawnTool()
    {
        // Wait for the respawn delay
        yield return new WaitForSeconds(respawnDelay);

        // If the tool hasn't been picked up again during the delay, respawn it
        if (!isHeld)
        {
            Respawn();
        }
    }

    // Function to move the tool back to its initial position and rotation
    private void Respawn()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;  // Reset velocity
            rb.angularVelocity = Vector3.zero;  // Reset angular velocity
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the events when this object is destroyed
        grabInteractable.selectExited.RemoveListener(OnRelease);
        grabInteractable.selectEntered.RemoveListener(OnGrab);
    }
}
