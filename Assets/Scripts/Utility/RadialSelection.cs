using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

// Code written following Valem Tutorials' video https://www.youtube.com/watch?v=n-xPN1v3dvA&t=152s

public class RadialSelection : MonoBehaviour
{
    // XR Interactor with Ray Interactor
    //public XRRayInteractor rayInteractor;
    public int numberOfRadialPart = 3;
    public GameObject radialPartPregab;
    public Transform radialPartCanvas;

    public float angleBetweenPart = 10;
    public Transform handTransform;

    public UnityEvent<int> OnPartSelected;

    private List<GameObject> spawnedParts = new List<GameObject>();
    private int currentSelectedRadialPart = -1;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SpawnRadialPart();
        GetSelectedRadialPart();
    }

    public void HideAndTriggerSelected()
    {
        OnPartSelected.Invoke(currentSelectedRadialPart);
        radialPartCanvas.gameObject.SetActive(false);
    }

    public void GetSelectedRadialPart()
    {
        Vector3 centerToHand = handTransform.position - radialPartCanvas.position;
        Vector3 centerToHandProjected = Vector3.ProjectOnPlane(centerToHand, radialPartCanvas.forward);

        float angle = Vector3.SignedAngle(radialPartCanvas.up, centerToHandProjected, -radialPartCanvas.forward);
        if (angle < 0)
        {
            angle += 360;
        }
        currentSelectedRadialPart = (int) angle * numberOfRadialPart / 360;

        for (int i = 0; i < numberOfRadialPart; i++)
        {
            if (i == currentSelectedRadialPart)
            {
                spawnedParts[i].GetComponent<Image>().color = Color.blue;
                spawnedParts[i].transform.localScale = 1.1f * Vector3.one;
            }
            else
            {
                spawnedParts[i].GetComponent<Image>().color = Color.white;
                spawnedParts[i].transform.localScale = 1f * Vector3.one;
            }
        }
    }

    public void SpawnRadialPart()
    {
        foreach (var item in spawnedParts)
        {
            Destroy(item);
        }
        
        spawnedParts.Clear();

        for (int i = 0; i < numberOfRadialPart; i++)
        {
            float angle = - i * 360 / numberOfRadialPart - angleBetweenPart / 2;
            Vector3 radialPartEulerAngle = new Vector3(0, 0, angle);
            GameObject spawnedRadialPart = Instantiate(radialPartPregab, radialPartCanvas);
            spawnedRadialPart.transform.position = radialPartCanvas.position;
            spawnedRadialPart.transform.localEulerAngles = radialPartEulerAngle;
            spawnedRadialPart.GetComponent<Image>().fillAmount = (1f / (float)numberOfRadialPart) - (angleBetweenPart / 360);
        
            spawnedParts.Add(spawnedRadialPart);
        
        }
    }
}
