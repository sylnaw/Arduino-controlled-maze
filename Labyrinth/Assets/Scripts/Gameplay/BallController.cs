using UnityEngine;

public class BallController : MonoBehaviour {

    public float multiplier = 5;

    new Rigidbody rigidbody;
    GameMenager menager;
    DataReciver data;

    void Start()
    {
        menager = GameObject.Find("GameMenager").GetComponent<GameMenager>();
        data = GameObject.Find("Arduino").GetComponent<DataReciver>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        while (!data.Computed) ;
        rigidbody.AddForce(data.AccY*multiplier, 0, -data.AccX*multiplier);
    }

    void OnTriggerEnter()
    {
        menager.LoadNextLevel();
    }
}
