using UnityEngine;

public enum MazeDirection
{
    North,
    East,
    South,
    West
}

public static class MazeDirections
{
    private static Vector2Int[] vectors = {
        new Vector2Int(0, -1),
        new Vector2Int(1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(-1, 0)
    };

    private static Quaternion[] rotations = {
        Quaternion.Euler(0, 180, 0),
        Quaternion.Euler(0, 90, 0),
        Quaternion.Euler(0, 0, 0),
        Quaternion.Euler(0, 270, 0)
    };

    public static Vector2Int MakeNewCoordinates(Vector2Int coordinates, MazeDirection direction)
    {
        Vector2Int randomDirection = vectors[(int)direction];
        coordinates.x += randomDirection.x;
        coordinates.y += randomDirection.y;
        return coordinates;
    }

    public static MazeDirection GetOpposite(this MazeDirection direction)
    {
        return (MazeDirection)(((int)direction + 2) % 4);
    }

    public static Quaternion ToRotation(this MazeDirection direction)
    {
        return rotations[(int)direction];
    }
}