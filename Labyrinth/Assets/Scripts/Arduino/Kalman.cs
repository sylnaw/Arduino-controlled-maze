using UnityEngine;

public class Kalman
{
    float Q_angle = 0.01f; 
    float Q_bias = 0.0003f; 
    float R_measure = 0.003f; 

    float angle = 90; 
    float bias = 0; 
    float rate; 

    float[,] P = new float[2, 2];
    float[] K = new float[2];

    float S, y;
    float dt; 

    public Kalman()
    {
        P[0, 0] = 0;
        P[0, 1] = 0;
        P[1, 0] = 0;
        P[1, 1] = 0;
    }

    public float KalmanUpdate(float newAcc, float newGyro/*newRate*/)
    {
        dt = Time.timeSinceLevelLoad - dt;
        Predict(newGyro);
        Update(newAcc);
        dt = Time.timeSinceLevelLoad;
        return angle;
    }

    void Predict(float newGyro)
    {
        rate = newGyro - bias;
        angle += dt * rate;

        P[0, 0] += dt * (P[1, 1] + P[0, 1]) + Q_angle * dt;
        P[0, 1] -= dt * P[1, 1];
        P[1, 0] -= dt * P[1, 1];
        P[1, 1] += Q_bias * dt;
    }

    void Update(float newAcc)
    {
        S = P[0, 0] + R_measure;

        K[0] = P[0, 0] / S;
        K[1] = P[1, 0] / S;

        y = newAcc - angle; 
        angle += K[0] * y;
        bias += K[1] * y;

        P[0, 0] -= K[0] * P[0, 0];
        P[0, 1] -= K[0] * P[0, 1];
        P[1, 0] -= K[1] * P[0, 0];
        P[1, 1] -= K[1] * P[0, 1];
    }
}
