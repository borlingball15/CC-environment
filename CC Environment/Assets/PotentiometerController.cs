using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotentiometerController : MonoBehaviour
{

    public Light areaLight;
    public float minBrightness = 0.1f;
    public float maxBrightness = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float potValue = AnalogInput.ReadValue();

        float brightness = Mathf.Lerp(minBrightness, maxBrightness, potValue);

        areaLight.intensity = brightness;
        
        if (potValue <= 0.01f)
        {
            areaLight.enabled = false; // Turn off the light
        }
        else
        {
            areaLight.enabled = true; // Turn on the light
        }
    }
}
