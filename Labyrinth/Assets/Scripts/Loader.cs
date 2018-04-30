using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public string scene;

    void Start()
    {
        SceneManager.LoadScene(scene);
    }
}
