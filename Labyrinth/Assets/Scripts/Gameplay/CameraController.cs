using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float height = 7;
    GameObject ball;

    void Start()
    {
        ball = GameObject.Find("Ball");
    }

    void Update()
    {
        transform.position = new Vector3(ball.transform.position.x,
            ball.transform.position.y + height, ball.transform.position.z);
    }
}
