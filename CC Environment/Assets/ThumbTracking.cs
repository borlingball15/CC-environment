using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ThumbTracking : MonoBehaviour
{
    public string portName = "/dev/cu.usbmodern1101"; // Change this to match the port your Arduino Uno is connected to
    public int baudRate = 9600;

    private SerialPort serialPort;

    // Camera rotation speed
    public float rotateSpeed = 1f;

    void Start()
    {
        // Open the serial port
        serialPort = new SerialPort(portName, baudRate);
        serialPort.Open();
    }

    void Update()
    {
        if (serialPort.IsOpen)
        {
            // Read the data from the serial port
            string data = serialPort.ReadLine();

            // Check if the data indicates right thumb placement
            if (data.Contains("RightThumb"))
            {
                // Rotate the camera right
                transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
            }
            // Check if the data indicates left thumb placement
            else if (data.Contains("LeftThumb"))
            {
                // Zoom in
                transform.Translate(Vector3.forward * rotateSpeed * Time.deltaTime);
            }
            // Check if the data indicates both thumbs placement
            else if (data.Contains("BothThumbs"))
            {
                // Zoom out to original position
                transform.Translate(Vector3.back * rotateSpeed * Time.deltaTime);
            }
        }
    }

    void OnDestroy()
    {
        // Close the serial port when the script is destroyed
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
