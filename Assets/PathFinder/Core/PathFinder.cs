using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder : IPathFinder
{
    public IEnumerable<Vector2> GetPath(Vector2 A, Vector2 C, IEnumerable<Edge> edges)
    {
        if (edges == null || !edges.Any())
        {
            return new List<Vector2>();
        }

        List<Vector2> path = new List<Vector2>();

        Vector2 currentPosition = A;

        path.Add(currentPosition);

        bool reachedGoal = false;

        foreach (var edge in edges)
        {
            if (IsOnEdge(currentPosition, edge))
            {
                currentPosition = edge.End;

                path.Add(currentPosition);
            }

            if (IsOnEdge(C, edge))
            {
                currentPosition = edge.End;

                path.Add(currentPosition);

                reachedGoal = true;

                break;
            }
        }

        if (!reachedGoal)
        {
            Debug.Log("The path was not found");
            return new List<Vector2>();
        }

        return path;
    }

    private bool IsOnEdge(Vector2 point, Edge edge)
    {
        return (point.x >= Mathf.Min(edge.Start.x, edge.End.x) &&
                point.x <= Mathf.Max(edge.Start.x, edge.End.x) &&
                point.y >= Mathf.Min(edge.Start.y, edge.End.y) &&
                point.y <= Mathf.Max(edge.Start.y, edge.End.y));
    }
}