using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class JoystickController : MonoBehaviour
{
    public string portName = "/dev/cu.usbmodern1101"; // Change this to match the port your Arduino Uno is connected to
    public int baudRate = 9600;

    private SerialPort serialPort;

    public float moveSpeed = 1f;
    public float zoomSpeed = 1f;

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
            if (data.StartsWith("Direction:Left"))
            {
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            }
            else if (data.StartsWith("Direction:Right"))
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }
            else if (data.StartsWith("Direction:Up"))
            {
                transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
            }
            else if (data.StartsWith("Direction:Down"))
            {
                transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
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

