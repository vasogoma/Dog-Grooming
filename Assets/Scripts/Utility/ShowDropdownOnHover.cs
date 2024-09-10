using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDropdownOnHover : MonoBehaviour
{
    public Canvas commandCanvas;  // The canvas holding the dropdown menu
    private bool isHovering = false;

    void Start()
    {
        commandCanvas.gameObject.SetActive(false);  // Hide canvas initially
    }

    void Update()
    {
        if (isHovering)
        {
            // Position the canvas in front of the user
            commandCanvas.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
            commandCanvas.transform.LookAt(Camera.main.transform.position); // Make sure the canvas faces the user
        }
    }

    public void OnPointerEnter()
    {
        isHovering = true;
        commandCanvas.gameObject.SetActive(true);
    }

    public void OnPointerExit()
    {
        isHovering = false;
        commandCanvas.gameObject.SetActive(false);
    }
}
