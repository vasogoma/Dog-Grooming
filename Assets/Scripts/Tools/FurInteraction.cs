using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurInteraction : MonoBehaviour
{
    
    public string type; // Type of the tool (soap, clippers)
    public ParticleSystem bubbleEffect;
    public AudioSource toolSound;

    int collisionsActive=0; // Number of active collisions with the fur

    public DogCommandManager dogCommandManager;
    

    private void OnTriggerEnter(Collider fur)
    {
        // Cut the fur using the clippers
        if (fur.CompareTag("Fur") && type == "clippers")
        {
            cutFur(fur);
        }
        // Clean the fur using the soap
        else if (fur.CompareTag("Fur") && type == "soap")
        {
            cleanFur(fur);
            // Play the soap sound effect
            PlayToolSound();
            collisionsActive++;
        }
    }

    // Stop the soap sound effect when interaction ends
    private void OnTriggerExit(Collider fur)
    {
        if (fur.CompareTag("Fur") && type == "soap")
        {
            collisionsActive--;
            if (collisionsActive == 0){
                StopToolSound();
            }
        }
    }

    private void cutFur(Collider fur)
    {
        FurStateController furStateController = dogCommandManager.currentDog.GetComponent<FurStateController>();
        if (furStateController != null)
        {
            GameObject[] furSpheres = furStateController.GetFurSpheres();
            int index = System.Array.IndexOf(furSpheres, fur.gameObject);

            if (index != -1){
                FurStateController.FurState furState = furStateController.GetFurState(index);

                // Check if fur is Clean and Dry
                if (furState == FurStateController.FurState.CleanDry)
                {
                    Rigidbody rb = fur.GetComponent<Rigidbody>();
                    if (rb == null)
                    {
                        rb = fur.gameObject.AddComponent<Rigidbody>();
                        rb.isKinematic = true;
                    }

                    rb.isKinematic = false;
                    int areaIndex = furStateController.GetAreaIndex(fur.gameObject);
                    if (areaIndex != -1)
                    {
                        furStateController.cutFurCount[areaIndex]++;
                    }
                    Destroy(fur.gameObject, 3f);
                }
            }
        }
    }

    private void cleanFur(Collider fur)
    {
        FurStateController furStateController = dogCommandManager.currentDog.GetComponent<FurStateController>();
        if (furStateController != null){
            GameObject[] furSpheres = furStateController.GetFurSpheres();
            int index = System.Array.IndexOf(furSpheres, fur.gameObject);

            if (index != -1)
            {
                FurStateController.FurState furState = furStateController.GetFurState(index);

                // Check if fur is DirtyWet or CleanWet
                if (furState == FurStateController.FurState.DirtyWet || furState == FurStateController.FurState.CleanWet)
                {
                    // Change fur state to Soapy
                    furStateController.SetFurState(index, FurStateController.FurState.Soapy);
                    // Trigger bubble particle effect
                    TriggerBubbleEffect(fur.transform);
                }
            }
        }
    }
    private void TriggerBubbleEffect(Transform furTransform)
    {
        // Instantiate the bubble particle effect at the fur's location
        if (bubbleEffect != null)
        {
            Instantiate(bubbleEffect, furTransform.position, Quaternion.identity);
        }
    }

    // Play the tool's sound effect
    private void PlayToolSound()
    {
        if (toolSound != null && !toolSound.isPlaying)
        {
            toolSound.Play();
        }
    }

    // Stop the sound effect when interaction ends
    private void StopToolSound()
    {
        if (toolSound != null && toolSound.isPlaying)
        {
            toolSound.Stop();  
        }
    }

}
