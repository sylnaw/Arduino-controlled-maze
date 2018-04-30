using System.Collections.Generic;
using UnityEngine;

public class MazeCreator : MonoBehaviour
{
    public GameObject wallPrefab;
    MazeCell[,] cells;
    List<MazeCell> activeCells = new List<MazeCell>();

    public void CreateMaze()
    {
        transform.GetChild(0).localScale = new Vector3(GameMenager.size.x, 1, GameMenager.size.y);
        cells = new MazeCell[GameMenager.size.x, GameMenager.size.y];
        activeCells.Add(CreateCell(RandomCoordinates));
        while (activeCells.Count > 0)
            DoNextStepOfBuilding();
    }

    MazeCell CreateCell(Vector2Int coordinates)
    {
        cells[coordinates.x, coordinates.y] = new MazeCell
        {
            position = new Vector3(coordinates.x - GameMenager.size.x * 0.5f + 0.5f, 0f, coordinates.y - GameMenager.size.y * 0.5f),
            coordinates = new Vector2Int(coordinates.x, coordinates.y)
        };
        return cells[coordinates.x, coordinates.y];
    }

    Vector2Int RandomCoordinates
    {
        get { return new Vector2Int(Random.Range(0, GameMenager.size.x), Random.Range(0, GameMenager.size.y)); }
    }

    void DoNextStepOfBuilding()
    {
        MazeCell currentCell = activeCells[activeCells.Count - 1];
        if (!currentCell.IsFullyInitialized)
        {
            MazeDirection direction;
            Vector2Int neighbourCoordinates;
            GetDataFromCurrentCell(currentCell, out direction, out neighbourCoordinates);
            CreateEdge(currentCell, direction, neighbourCoordinates);
        }
        else activeCells.RemoveAt(activeCells.Count - 1);
    }

    void GetDataFromCurrentCell(MazeCell currentCell, out MazeDirection direction, out Vector2Int neighbourCoordinates)
    {
        direction = currentCell.RandomUninitializedDirection;
        neighbourCoordinates = MazeDirections.MakeNewCoordinates(currentCell.coordinates, direction);
        currentCell.RemoveEdge(direction);
    }

    void CreateEdge(MazeCell currentCell, MazeDirection direction, Vector2Int neighbourCoordinates)
    {
        if (CheckIfMazeContainsCoordinates(neighbourCoordinates))
        {
            if (cells[neighbourCoordinates.x, neighbourCoordinates.y] != null)
            {
                CreateWall(currentCell, cells[neighbourCoordinates.x, neighbourCoordinates.y], direction);
                cells[neighbourCoordinates.x, neighbourCoordinates.y].RemoveEdge(direction.GetOpposite());
            }
            else
                CreateNeighbour(neighbourCoordinates, direction);
        }
        else
            CreateWall(currentCell, null, direction);
    }

    public bool CheckIfMazeContainsCoordinates(Vector2Int coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < GameMenager.size.x && coordinate.y >= 0 && coordinate.y < GameMenager.size.y;
    }

    void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        GameObject wall = Instantiate(wallPrefab);
        wall.transform.parent = gameObject.transform;
        wall.transform.localPosition = cell.position;
        wall.transform.localRotation = direction.ToRotation();
    }

    void CreateNeighbour(Vector2Int neighbourCoordinates, MazeDirection direction)
    {
        MazeCell neighbour = CreateCell(neighbourCoordinates);
        neighbour.RemoveEdge(direction.GetOpposite());
        activeCells.Add(neighbour);
    }
}
