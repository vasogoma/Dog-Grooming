using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFall : MonoBehaviour
{
    public ParticleSystem waterfallEffect;
    public GameObject waterDrop;
    private Object[] waterDrops;

    // Shower head position
    float x = 2.9f;
    float y = 3.9f;
    float z = -5.7f;

    int currentWaterDrop = 0;

    bool isEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        waterDrops = new Object[100];
    }

    // Update is called once per frame
    void Update()
    {
        // If the waterfall is enabled, create water drops
        if (isEnabled)
        {
            if (waterDrops[currentWaterDrop] != null)
            {
                destroyWaterDrop();
            }
            createWaterDrop();
            if (currentWaterDrop == 100)
            {
                currentWaterDrop = 0;
            }
        }
        
    }

    public void createWaterDrop()
    {
        // Use radius of shower head to spawn water drops
        float xSpawn = x + Random.Range(-0.5f, 0.5f);
        float zSpawn = z + Random.Range(-0.5f, 0.5f);

        waterDrops[currentWaterDrop] = Instantiate(waterDrop, new Vector3(xSpawn, y, zSpawn), Quaternion.identity);

        // Make water drop fall
        Rigidbody rb = ((GameObject)waterDrops[currentWaterDrop]).GetComponent<Rigidbody>();
        rb.useGravity = true;
        currentWaterDrop++;
    }

    // Destroy the water drop
    public void destroyWaterDrop()
    {
        Destroy(waterDrops[currentWaterDrop]);
    }

    // Toggle the waterfall effect
    public void toggleWaterfall()
    {
        isEnabled = !isEnabled;
        if (isEnabled)
        {
            TriggerWaterfallEffect();
        }
    }

    // Trigger the waterfall effect
    private void TriggerWaterfallEffect()
    {
        // Instantiate the waterfall particle effect at the showerhead position (x, y, z)
        if (waterfallEffect != null)
        {
            // Use a specific rotation for the waterfall effect to make it look like it's coming from the shower head in an angle to hit the dog
            Instantiate(waterfallEffect, new Vector3(x,y,z), new Quaternion(-0.6963f, -0.1227f, 0.1227f, 0.6963f));
        }
        else
        {
            Debug.LogWarning("Waterfall particle effect is not assigned.");
        }
    }

    
}
