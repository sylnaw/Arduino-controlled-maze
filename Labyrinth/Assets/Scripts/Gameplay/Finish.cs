using UnityEngine;

public class Finish : MonoBehaviour
{
    void OnEnable()
    {
        transform.parent = GameObject.Find("Maze").transform;
        SetPosition();
    }

    void SetPosition()
    {
        float x = GameMenager.size.x, z = GameMenager.size.y;
        RandomCoordinates(ref x, ref z);
        transform.position = new Vector3(x, 0, z);
    }

    void RandomCoordinates(ref float x, ref float z)
    {
        if (Random.Range(0, 2) == 0) SetCoordinates(ref x, ref z);
        else SetCoordinates(ref z, ref x);
    }

    void SetCoordinates(ref float firstNumber, ref float secondNumber)
    {
        firstNumber = (int)(firstNumber / 2);
        secondNumber = Random.Range(0, (int)(secondNumber / 2));
    }
}
