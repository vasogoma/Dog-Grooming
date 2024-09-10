using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Blowdryer : MonoBehaviour
{   
    public ToolRelease toolRelease;

    public ParticleSystem vaporEffect;

    int collisionsActive = 0; // Number of active wet fur being dried

    private bool isPlayingEffects = false;  // To check if effects are currently playing

    public DogCommandManager dogCommandManager;

    void Start()
    {
    }

    void Update()
    {
        // Check if the tool is held
        if (toolRelease.isHeld)
        {
            FurStateController furStateController = dogCommandManager.currentDog.GetComponent<FurStateController>();
            GameObject[] furSpheres = furStateController.GetFurSpheres();
            bool foundWetFur = false; // check if any wet fur is found

            foreach (GameObject fur in furSpheres)
            {
                int index = System.Array.IndexOf(furSpheres, fur.gameObject);
                FurStateController.FurState furState = furStateController.GetFurState(index);

                // Check if fur is Wet
                if (furState == FurStateController.FurState.CleanWet)
                {
                    // Check if fur is close to the blowdryer
                    if (Vector3.Distance(fur.transform.position, transform.position) < 1.0f)
                    {
                        Vector3 direction = fur.transform.position - transform.position;
                        // Check if fur is within the blowdryer's cone of effect rotated by 90 degrees 
                        float angle = Vector3.Angle(direction, transform.forward);
                        if (angle < 120 && angle > 60)
                        {
                            // Set fur to CleanDry
                            furStateController.SetFurState(index, FurStateController.FurState.CleanDry);
                            foundWetFur = true;

                            // Play the blowdryer sound effect and particle effect
                            if (!isPlayingEffects)  // Check the boolean flag instead of the method
                            {
                                Debug.Log("Playing effects");
                                PlayEffects();  // Call the method
                            }
                        }
                    }
                }
            }

            // Stop the blowdryer sound effect and particle effect
            if (!foundWetFur && isPlayingEffects)  // Check the boolean flag instead of the method
            {
                Debug.Log("Stopping blowdryer effects...");
                StopEffects();
            }
        }
        else if (isPlayingEffects)  // Check the boolean flag instead of the method
        {
            Debug.Log("Stopping effects after tool is not held");
            StopEffects();  // Call the method
        }
    }

    private void PlayEffects()
    {
        if (vaporEffect != null && !vaporEffect.isPlaying)
        {
            vaporEffect.Play();
            Debug.Log("Vapor effect started");
        }
        else if (vaporEffect == null)
        {
            Debug.LogWarning("Vapor effect is not assigned!");
        }

        isPlayingEffects = true;
    }

    private void StopEffects()
    {
        if (vaporEffect != null && vaporEffect.isPlaying)
        {
            vaporEffect.Stop();
            Debug.Log("Vapor effect stopped");
        }

        isPlayingEffects = false;
    }
}