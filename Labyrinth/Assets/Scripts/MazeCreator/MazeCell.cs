using System.Collections.Generic;
using UnityEngine;

public class MazeCell
{
    public Vector2Int coordinates;
    public Vector3 position;
    public bool IsFullyInitialized { get { return edges.Count == 0; } }
    public MazeDirection RandomUninitializedDirection
    {
        get { return edges[Random.Range(0, edges.Count)]; }
    }
    List<MazeDirection> edges = new List<MazeDirection>();

    public MazeCell()
    {
        edges.Add(MazeDirection.North);
        edges.Add(MazeDirection.East);
        edges.Add(MazeDirection.South);
        edges.Add(MazeDirection.West);
    }

    public void RemoveEdge(MazeDirection direction)
    {
        edges.Remove(direction);
    }
}
