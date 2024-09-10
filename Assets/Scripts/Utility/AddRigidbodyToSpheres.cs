using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipperInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fur"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = other.gameObject.AddComponent<Rigidbody>(); // Add Rigidbody if not already present
                rb.isKinematic = true; // Start as kinematic to prevent immediate movement
            }

            rb.isKinematic = false; // Allow physics to affect the fur sphere
            Destroy(rb.gameObject, 3f); // Destroy the fur sphere after 3 seconds
        }
    }
}
