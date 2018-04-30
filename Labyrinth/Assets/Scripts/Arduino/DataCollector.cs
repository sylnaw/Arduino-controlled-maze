using System.IO;
using System.IO.Ports;
using System.Threading;
using UnityEngine;

public class DataCollector
{
    public static string port = "COM6";
    public static int baundRate = 115200;
    public string Data { get; private set; }

    SerialPort serialPort = new SerialPort();
    Thread thread;
    bool connected = false;
    bool appRunning = true;

    public DataCollector()
    {
        foreach (string port in SerialPort.GetPortNames())
        {
            try
            {
                serialPort.PortName = port;
                serialPort.BaudRate = baundRate;
                serialPort.Open();
            }
            catch (IOException e)
            {
                Debug.Log(e.Message);
            }
        }
        CheckConnection();
        thread = new Thread(Receive);
    }

    public void Start()
    {
        thread.Start();
    }

    public void Restart()
    {
        if (!serialPort.IsOpen)
        {
            try
            {
                serialPort.PortName = port;
                serialPort.BaudRate = 115200;
                serialPort.Open();
            }
            catch (IOException e)
            {
                Debug.Log(e.Message);
            }
            CheckConnection();
        }
    }

    void Receive()
    {
        while (connected && appRunning)
        {
            string tempData = serialPort.ReadLine();
            if (tempData[0] == '!') Data = tempData;
            CheckConnection();
        }
        thread.Abort();
    }

    void CheckConnection()
    {
        if (serialPort.IsOpen) connected = true;
        else connected = false;
    }

    public void Stop()
    {
        appRunning = false;
        serialPort.Close();
        CheckConnection();
    }
}