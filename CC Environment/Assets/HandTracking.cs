using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class HandTracking : MonoBehaviour
{
    public string portName = "/dev/cu.usbmodern1101"; // Change this to match the port your Arduino Uno is connected to
    public int baudRate = 9600;

    private SerialPort serialPort;

    // Camera movement speed
    public float moveSpeed = 1f;

    // Camera rotation speed
    public float rotateSpeed = 1f;

    void Start()
    {
        serialPort = new SerialPort(portName, baudRate);
        serialPort.Open();
    }

    void Update()
    {
        if (serialPort.IsOpen)
        {
            string data = serialPort.ReadLine();
            if (data.StartsWith("R:"))
            {
                int rightValue = int.Parse(data.Substring(2));
                if (rightValue == 1)
                {
                    // Move camera right
                    transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                }
            }
            else if (data.StartsWith("L:"))
            {
                int leftValue = int.Parse(data.Substring(2));
                if (leftValue == 1)
                {
                    // Move camera left
                    transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                }
            }
            else if (data.StartsWith("D:"))
            {
                float distance = float.Parse(data.Substring(2));
                // Adjust camera position based on distance
                float zMovement = (15f - distance) * moveSpeed * Time.deltaTime;
                transform.Translate(Vector3.forward * zMovement);
            }
        }
    }

    void OnDestroy()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
