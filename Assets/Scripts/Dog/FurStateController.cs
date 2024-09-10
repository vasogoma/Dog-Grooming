using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FurStateController : MonoBehaviour
{
    public enum FurState
    {
        CleanDry,
        CleanWet,
        Soapy,
        DirtyDry,
        DirtyWet
    }

    public Material defaultMat;
    public Material wetFurMat;
    public Material dirtyMat;

    public Material soapyMat;

    private FurState[] furStates;
    private GameObject[] furSpheres;
    public DogCommandManager dogCommandManager;

    
    public int[] countFurTypes; // 0 is clean dry, 1 is clean wet, 2 is soapy, 3 is dirty dry, 4 is dirty wet

    public int[] totalFurCount; // 0 is head, 1 is ears, 2 is body, 3 is legs, 4 is tail

    public int[] cutFurCount; // 0 is head, 1 is ears, 2 is body, 3 is legs, 4 is tail

    void Start()
    {
        countFurTypes = new int[5];
        totalFurCount = new int[5];
        cutFurCount = new int[5];
        // Find all fur spheres that belong to active dog
        GameObject[] furSpheresAll = GameObject.FindGameObjectsWithTag("Fur");

        furSpheres = new GameObject[furSpheresAll.Length/3];
        int j = 0;
        for (int i = 0; i < furSpheresAll.Length; i++)
        {
            if (furSpheresAll[i].transform.root.gameObject.name == name)
            {
                furSpheres[j] = furSpheresAll[i];
                j++;   
                int areaIndex = GetAreaIndex(furSpheresAll[i]);
                if (areaIndex != -1)
                {
                    totalFurCount[areaIndex]++;
                }
            }
            
        }
        furStates = new FurState[furSpheres.Length];

        // Initialize fur states randomly
        for (int i = 0; i < furStates.Length; i++)
        {
            if (Random.value < 0.25f)
            {
                furStates[i] = FurState.DirtyDry;
                countFurTypes[3]++;
            }
            else
            {
                furStates[i] = FurState.CleanDry;
                countFurTypes[0]++;
            }
            UpdateMaterial(furSpheres[i], furStates[i]);
        }
    }

    public int GetAreaIndex(GameObject fur)
    {

        // Get the parent of the parent of the fur sphere
        Transform furTf = fur.transform;
        Transform furParentTf = fur.transform.parent;
        GameObject furParent = fur.transform.parent.gameObject;
        GameObject furGrandParent = furParent.transform.parent.gameObject;
        string parentName = furGrandParent.name;
        if (parentName == "Fur Top Head")
        {
            return 0;
        }
        else if (parentName == "ears")
        {
            return 1;
        }
        else if (parentName == "Fur Body L" || parentName == "Fur Body R")
        {
            return 2;
        }
        else if (parentName == "Front leg R" || parentName == "Front leg L" || parentName == "Rear leg R" || parentName == "Rear leg L")
        {
            return 3;
        }
        else if (parentName == "Tail")
        {
            return 4;
        }
        else
        {
            return -1;
        }
    }
    public FurState GetFurState(int index)
    {
        if (index >= 0 && index < furStates.Length)
        {
            return furStates[index];
        }
        else
        {
            return FurState.CleanDry; // Default fallback
        }
    }

    public void SetFurState(int index, FurState newState)
    {
        FurState oldState = GetFurState(index);
        if (oldState == FurState.CleanDry)
        {
            countFurTypes[0]--;
        }
        else if (oldState == FurState.CleanWet)
        {
            countFurTypes[1]--;
        }
        else if (oldState == FurState.Soapy)
        {
            countFurTypes[2]--;
        }
        else if (oldState == FurState.DirtyDry)
        {
            countFurTypes[3]--;
        }
        else if (oldState == FurState.DirtyWet)
        {
            countFurTypes[4]--;
        }

        if (newState == FurState.CleanDry)
        {
            countFurTypes[0]++;
        }
        else if (newState == FurState.CleanWet)
        {
            countFurTypes[1]++;
        }
        else if (newState == FurState.Soapy)
        {
            countFurTypes[2]++;
        }
        else if (newState == FurState.DirtyDry)
        {
            countFurTypes[3]++;
        }
        else if (newState == FurState.DirtyWet)
        {
            countFurTypes[4]++;
        }

        if (index >= 0 && index < furStates.Length)
        {
            furStates[index] = newState;
            UpdateMaterial(furSpheres[index], newState);
        }
    }

    public GameObject[] GetFurSpheres()
    {
        return furSpheres;
    }

    private void UpdateMaterial(GameObject furSphere, FurState state)
    {
        if (furSphere == null)
        {
            return;
        }
        Renderer renderer = furSphere.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material mat = GetMaterialForState(state);
            renderer.material = mat;
        }
    }

    private Material GetMaterialForState(FurState state)
    {
        switch (state)
        {
            case FurState.CleanDry:
                return defaultMat;
            case FurState.CleanWet:
                return wetFurMat;
            case FurState.Soapy:
                return soapyMat;
            case FurState.DirtyDry:
                return dirtyMat;
            case FurState.DirtyWet:
                return dirtyMat;
            default:
                return null;
        }
    }

    void Update()
    {
        // Change all dog fur to clean dry when space is pressed for testing
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SetAllFurState(FurState.CleanWet);
        }
    }

    public void SetAllFurState(FurState newState)
    {
        for (int i = 0; i < furStates.Length; i++)
        {
            SetFurState(i, newState);
        }
    }

    public void setFurCleanDry()
    {
        SetAllFurState(FurState.CleanDry);
    }
}
