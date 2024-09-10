using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterButton : MonoBehaviour
{
    public WaterFall waterFall;
    public bool canToggle = true;

    public DogCommandManager dogCommandManager;

    public AudioSource waterSound;

    // Start is called before the first frame update
    void Start()
    {
        waterSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When the button is clicked, the water will start falling
    public void Select()
    {
        if (canToggle)
        {
            //Debug.Log("Triggered with: " + other.gameObject.name);
            waterFall.toggleWaterfall();
            canToggle = false;
            // Transform the button by lowering its position in y-axis
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            StartCoroutine(ResetButton());
            StartCoroutine(CleanDog());
            waterSound.Play();
        }
    }

    // Reset the button after 5 seconds
    IEnumerator ResetButton()
    {
        yield return new WaitForSeconds(5);
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        waterFall.toggleWaterfall();
        canToggle = true;
        waterSound.Stop();
    }

    // Wet the dog when the water is falling
    IEnumerator CleanDog()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(1);
        // Check if the dog is in the bathtub
        if (dogCommandManager.currentPosition == "bathtub")
        {
            FurStateController furStateController = dogCommandManager.currentDog.GetComponent<FurStateController>();

            for (int i = 0; i < furStateController.GetFurSpheres().Length; i++)
            {
                if (i % 5 == 0)
                {
                    yield return new WaitForSeconds(0.02f);
                }
                FurStateController.FurState furState = furStateController.GetFurState(i);
                if (furState == FurStateController.FurState.DirtyDry)
                {
                    furStateController.SetFurState(i, FurStateController.FurState.DirtyWet);
                }
                if (furState == FurStateController.FurState.CleanDry)
                {
                    furStateController.SetFurState(i, FurStateController.FurState.CleanWet);
                }
                if (furState == FurStateController.FurState.Soapy)
                {
                    furStateController.SetFurState(i, FurStateController.FurState.CleanWet);
                }
            }
        }
    }
}
