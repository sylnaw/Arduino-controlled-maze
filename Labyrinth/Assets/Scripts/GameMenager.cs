using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenager : MonoBehaviour
{
    public static Vector2Int size = new Vector2Int(11, 5);
    public static int level = 1;
    public Finish finish;
    public MazeCreator mazeCreator;

    void Start()
    {
        mazeCreator.CreateMaze();
        Instantiate(finish);
    }

    public void LoadNextLevel()
    {
        level++;
        size = new Vector2Int(size.x + 4, size.y + 2);
        SceneManager.LoadScene("Labyrinth");
    }
}
