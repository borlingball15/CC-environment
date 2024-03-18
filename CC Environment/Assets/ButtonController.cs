using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private bool lightsOn = false;
    private bool prevButtonState = false;

    void Update()
    {
        if (UduinoManager.Instance.HasData(0))
        {
            string data = UduinoManager.Instance.ReadFromDevice(0);
            bool buttonState = data.Trim() == "1";
            if (buttonState != prevButtonState)
            {
                ToggleLights();
                prevButtonState = buttonState;
            }
        }
    }

    void ToggleLights()
    {
        lightsOn = !lightsOn;
        // Toggle your lights here
        if (lightsOn)
        {
            Debug.Log("Lights On");
            // Turn on the lights
        }
        else
        {
            Debug.Log("Lights Off");
            // Turn off the lights
        }
    }
}
