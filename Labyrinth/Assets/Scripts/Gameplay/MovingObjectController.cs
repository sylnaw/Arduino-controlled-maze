using UnityEngine;

public class MovingObjectController : MonoBehaviour
{
    public int maxAngle = 45;
    public float multiplier = 0.25f;
    public float initialX = 0, initialY = 0;

    DataReciver data;
    float lastRoll = 0, lastPitch = 0;


    void Start()
    {
        data = GameObject.Find("Arduino").GetComponent<DataReciver>();
    }

    void Update()
    {
        while (!data.Computed) ;
        float roll, pitch;
        GetRollAndPitch(out roll, out pitch);
        SetMaximumNumber(ref roll, maxAngle);
        SetMaximumNumber(ref pitch, maxAngle);
        PreventSkipping(ref roll, ref pitch);
        transform.eulerAngles = new Vector3(initialX + roll * multiplier, 0, initialY + pitch * multiplier);
        SetBackupVariables(roll, pitch);
    }

    void GetRollAndPitch(out float roll, out float pitch)
    {
        roll = data.Roll - 90;
        pitch = data.Pitch - 90;
    }

    void SetMaximumNumber(ref float number, int maximum)
    {
        if (number > maximum)
            number = maximum;
        if (number < -maximum)
            number = -maximum;
    }

    void PreventSkipping(ref float roll, ref float pitch)
    {
        if (!data.Up)
        {
            pitch = lastPitch;
            roll = lastRoll;
        }
    }

    void SetBackupVariables(float roll, float pitch)
    {
        lastRoll = roll;
        lastPitch = pitch;
    }
}
