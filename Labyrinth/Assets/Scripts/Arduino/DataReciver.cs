using System;
using UnityEngine;

public class DataReciver : MonoBehaviour
{
    const int ACC_RANGE = 2;
    const int GYRO_RANGE = 250;
    const int INT16MAX = 32767;

    public DataCollector dataCollector;
    public bool Computed { get; private set; }
    public float AccX { get; private set; }
    public float AccY { get; private set; }
    public bool Up { get; private set; }
    public float Roll { get; private set; }
    public float Pitch { get; private set; }

    float[] accData = new float[3];
    float[] gyroData = new float[3];
    Kalman KalmanPitch, KalmanRoll;

    void Awake()
    {
        KalmanPitch = new Kalman();
        KalmanRoll = new Kalman();
        dataCollector = new DataCollector();
        dataCollector.Start();
    }

    private void Update()
    {
        Computed = false;
        GetData();
        Computed = true;
        dataCollector.Restart();
    }

    void GetData()
    {
        string imuData = dataCollector.Data;
        if (imuData != null)
        {
            GetRawData(imuData);
            ConvertRawData();
        }
    }

    void GetRawData(string imuData)
    {
        imuData = imuData.Trim('!');

        string[] splitedImuData = imuData.Split(';');
        string[] splitedAccData = splitedImuData[0].Split(',');
        string[] splitedGyroData = splitedImuData[1].Split(',');

        ParseOneTypeOfDataFromImu(splitedAccData, accData);
        ParseOneTypeOfDataFromImu(splitedGyroData, gyroData);
    }

    void ParseOneTypeOfDataFromImu(string[] data, float[] parsedData)
    {
        for (int i = 0; i < 3; i++)
        {
            try { parsedData[i] = float.Parse(data[i]); }
            catch (FormatException e)
            {
                Debug.Log(e.Message + ", string: " + data[i]);
            }
        }
    }

    void ConvertRawData()
    {
        float z;
        ConvertToUnits(out z);
        Normalize(AccX, AccY, z);
        Estimate();
        Up = z > 0;
    }

    void ConvertToUnits(out float z)
    {
        AccX = ComputeDataFromImu(accData[0], ACC_RANGE);
        AccY = ComputeDataFromImu(accData[1], ACC_RANGE);
        z = ComputeDataFromImu(accData[2], ACC_RANGE);
    }

    void Normalize(float x, float y, float z)
    {
        float sqrt = Mathf.Sqrt((x * x) + (y * y) + (z * z));
        Pitch = Mathf.Acos(y / sqrt) * Mathf.Rad2Deg;
        Roll = Mathf.Acos(x / sqrt) * Mathf.Rad2Deg;
    }

    void Estimate()
    {
        if (!float.IsNaN(Pitch))
            Pitch = KalmanPitch.KalmanUpdate(Pitch, ComputeDataFromImu(gyroData[1], GYRO_RANGE));
        if (!float.IsNaN(Roll))
            Roll = KalmanRoll.KalmanUpdate(Roll, ComputeDataFromImu(gyroData[0], GYRO_RANGE));
    }

    float ComputeDataFromImu(float imuData, int range)
    {
        return (imuData * range) / INT16MAX;
    }

    void OnApplicationQuit()
    {
        dataCollector.Stop();
    }
}
