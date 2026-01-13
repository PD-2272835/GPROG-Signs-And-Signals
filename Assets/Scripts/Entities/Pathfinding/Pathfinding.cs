using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinding
{   
    const int DIAGONAL_MOVE_COST = 14;
    const int CARDINAL_MOVE_COST = 10;
  
    public static Pathfinding Instance { get; private set; }

    public CustomGrid<PathNode> Grid;
    public List<PathNode> Open;
    public List<PathNode> Closed;



    public Pathfinding(int width, int height)
    {
        Instance = this;
        Grid = new CustomGrid<PathNode>(width, height, 1.0f, Vector3.zero, (CustomGrid<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }


    public List<Vector3> FindPath(Vector3 startWorldPosition,  Vector3 targetWorldPosition)
    {
        var startNode = Grid.GetCellValue(startWorldPosition);
        var targetNode = Grid.GetCellValue(targetWorldPosition);
        List<PathNode> path = FindPath(startNode.x, startNode.y, targetNode.x, targetNode.y);
        List<Vector3> result = new List<Vector3>();

        if (path != null)
        {
            foreach (var node in path)
            {
                result.Add(node.GetWorldPos());
            }
            return result;
        }
        return null;
    }


    public List<PathNode> FindPath(int startX, int startY, int targetX, int targetY)
    {
        PathNode startNode = Grid.GetCellValue(startX, startY);
        PathNode targetNode = Grid.GetCellValue(targetX, targetY);

        Open = new List<PathNode>() { startNode };
        Closed = new List<PathNode>();

        for (int x = 0; x < Grid.GetWidth(); x++) {
            for (int y = 0; y < Grid.GetHeight(); y++)
            {
                PathNode pathNode = Grid.GetCellValue(x, y);
                pathNode.SetGCost(int.MaxValue);
                pathNode.SetFromNode(null);
            }
        }

        startNode.SetGCost(0);
        startNode.SetHCost(CalculateDistanceCost(startNode, targetNode));


        while (Open.Any())
        {
            PathNode currentNode = ChooseNodeToExplore();
            Debug.Log(currentNode);

            if (currentNode == targetNode) return ReconstructPath(targetNode); //if the path has been found, return it
         

            Closed.Add(currentNode);
            Open.Remove(currentNode);
            

            foreach (var neighbour in currentNode.GetNeighbours())
            {
                if (Closed.Contains(neighbour)) continue; //ensure the neighbour has not already been considered (still possible to move from closed to open if another path to it is found)
                if (!neighbour.isWalkable && neighbour != targetNode) //ensure the neighbour can be traversed
                {
                    Closed.Add(neighbour);
                    continue;
                }

                int tempGCost = currentNode.GCost + CalculateDistanceCost(currentNode, neighbour); //calculate "tentative cost"

                if (tempGCost < neighbour.GCost) //select the node with the lowest Gcost
                {
                    neighbour.SetFromNode(currentNode);
                    neighbour.SetGCost(tempGCost);
                    neighbour.SetHCost(CalculateDistanceCost(neighbour, targetNode));

                    if (!Open.Contains(neighbour))
                    {
                        Open.Add(neighbour);
                    }
                }
            }

        }

        //Entire grid was searched, no path found
        return null;
    }


    
    private PathNode ChooseNodeToExplore() //evaluate which node to explore
    {
        var current = Open[0];
        foreach (var other in Open) //other is a different node we evaluate from the open list
        {
            if (other.FCost < current.FCost || (other.FCost == current.FCost && other.HCost < current.HCost)) //choose either Node with lowest Fcost, if there are multiple of same Fcost, node with both lowest F and H cost
            {
                current = other;
            }
        }
        return current;
    }


    public List<PathNode> ReconstructPath(PathNode endNode) //return the found path
    {
        Debug.Log("Found Path");
        var currentNode = endNode;
        List<PathNode> path = new List<PathNode>();
        while (currentNode.fromNode != null)
        {
            path.Append(currentNode);
            currentNode = currentNode.fromNode;
        }
        path.Reverse();
        return path;
    }


    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return DIAGONAL_MOVE_COST * Mathf.Min(xDistance, yDistance) + CARDINAL_MOVE_COST * remaining;
    }
}
