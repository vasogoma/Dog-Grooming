using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For working with UI elements

public class DropdownMenuController : MonoBehaviour
{
    public Dropdown dropdown; // Reference to your Dropdown UI element
    public DogCommandManager dogCommandManager; // Reference to your DogCommandManager
    public GameObject commandCanvas; // Reference to the Canvas containing the Dropdown

    private void Start()
    {
        // Initially hide the dropdown menu
        commandCanvas.SetActive(false);

        // Add listener for dropdown value changes
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    public void ShowDropdown(bool show)
    {
        commandCanvas.SetActive(show);
    }

    private void OnDropdownValueChanged(int index)
    {
        // Handle dropdown value change
        switch (index)
        {
            case 0:
                dogCommandManager.GoToBathtub();
                
                break;
            case 1:
                dogCommandManager.GoToTable();
                break;
            case 2:
                dogCommandManager.GoToWaitingArea();
                break;
            default:
                Debug.LogWarning("Unhandled dropdown index: " + index);
                break;
        }
    }
}
