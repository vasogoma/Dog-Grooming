using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
//Add common usages
using UnityEngine.XR;
using UnityEngine.UI;

public class RaycastMenuController : MonoBehaviour
{
    public XRRayInteractor rayInteractor;

    public GameObject radialCanvas; // Reference to the menu canvas

    public XRController controller; // Reference to the XRController

    bool isHovering = false;

    float currentAngle = 0;

    public GameObject bathtubButton;
    public GameObject waitingAreaButton;
    public GameObject tableButton;
    public DogCommandManager dogCommandManager;

    public GameObject hovered;

    private void Start()
    {
        // Initially hide the dropdown menu
        radialCanvas.SetActive(false);
        rayInteractor.hoverEntered.AddListener(OnHoverEntered);
        rayInteractor.hoverExited.AddListener(OnHoverExited);
    }
    private void Update()
    {
        if (isHovering){
            // Get the joystick input angle using the Input System
            float angle = controller.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue) ? Mathf.Atan2(primary2DAxisValue.y, primary2DAxisValue.x) * Mathf.Rad2Deg : 0;
            // Get the raw angle of the joystick input
            float rawAngle = Mathf.Atan2(primary2DAxisValue.y, primary2DAxisValue.x) * Mathf.Rad2Deg;
            
            if (primary2DAxisValue.x == 0 && primary2DAxisValue.y == 0)
            {
                // Returned to rest
                if (currentAngle != 0)
                {
                    if (currentAngle > 0 && currentAngle < 120)
                    {
                        dogCommandManager.changeDog(hovered);
                        dogCommandManager.GoToBathtub();
                    }
                    else if (currentAngle >= 120 || currentAngle <= -120)
                    {
                        dogCommandManager.changeDog(hovered);
                        dogCommandManager.GoToWaitingArea();
                    }
                    else
                    {
                        dogCommandManager.changeDog(hovered);
                        dogCommandManager.GoToTable();
                    }
                    bathtubButton.GetComponent<Image>().color = Color.white;
                    waitingAreaButton.GetComponent<Image>().color = Color.white;
                    tableButton.GetComponent<Image>().color = Color.white;
                    currentAngle = 0;

                }
            }
            else
            {
                // Get the angle of the joystick input
                currentAngle = rawAngle;
                if (currentAngle > 0 && currentAngle < 120)
                {
                    bathtubButton.GetComponent<Image>().color = Color.grey;
                    waitingAreaButton.GetComponent<Image>().color = Color.white;
                    tableButton.GetComponent<Image>().color = Color.white;
                }
                else if (currentAngle >= 120 || currentAngle <= -120)
                {
                    bathtubButton.GetComponent<Image>().color = Color.white;
                    waitingAreaButton.GetComponent<Image>().color = Color.grey;
                    tableButton.GetComponent<Image>().color = Color.white;
                }
                else
                {
                    bathtubButton.GetComponent<Image>().color = Color.white;
                    waitingAreaButton.GetComponent<Image>().color = Color.white;
                    tableButton.GetComponent<Image>().color = Color.grey;
                }
            }

            //Get the pressed buttons
            if (controller.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
            {
                dogCommandManager.rotateLeft(dogCommandManager.currentDog);
            }
            if (controller.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonValue) && secondaryButtonValue)
            {
                dogCommandManager.rotateRight(dogCommandManager.currentDog);
            }
        }
        
        
        // Check if the raycast is hovering over the dog
        if (rayInteractor.TryGetCurrentUIRaycastResult(out var result))
        {
            Debug.Log(result.gameObject);
            if (result.gameObject.CompareTag("Dog"))
            {
                // Show the canvas menu
                radialCanvas.SetActive(true);
            }
            else
            {
                // Hide the dropdown menu if not hovering over the dog
                radialCanvas.SetActive(false);
            }
        }
    }
    
    public void OnHoverEntered(HoverEnterEventArgs args){
        if (dogCommandManager.canChangeDog || args.interactable.gameObject == dogCommandManager.currentDog)
        {
            if(args.interactable.gameObject.CompareTag("Dog"))
            {
                radialCanvas.SetActive(true);
                isHovering = true;
                hovered = args.interactable.gameObject;
            }
        }
        
    }

    public void OnHoverExited(HoverExitEventArgs args)
    {
        if (dogCommandManager.canChangeDog || args.interactable.gameObject == dogCommandManager.currentDog)
        {
            if(args.interactable.gameObject.CompareTag("Dog"))
            {
                radialCanvas.SetActive(false);
                isHovering = false;
            }
        }
    }

    
}
