using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Pathfinding
{
    CustomGrid<PathNode> Grid;
    public List<PathNode> Open;
    public List<PathNode> Closed;


    public Pathfinding(int width, int height)
    {
        Grid = new CustomGrid<PathNode>(width, height, 1.0f, Vector3.zero, (CustomGrid<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }


    public List<PathNode> FindPath(int startX, int startY, int targetX, int targetY)
    {
        PathNode startNode = Grid.GetCellValue(startX, startY);
        PathNode targetNode = Grid.GetCellValue(targetX, targetY);

        Open = new List<PathNode>() { startNode };
        Closed = new List<PathNode>();

        for (int x =0; x < Grid.GetWidth(); x++) {
            for (int y =0; y < Grid.GetHeight(); y++)
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

            if (currentNode == targetNode) return ReconstructPath(targetNode); //if the path has been found, return it

            Closed.Add(currentNode);
            Open.Remove(currentNode);
            

            foreach (var neighbour in currentNode.GetNeighbours())
            {
                if (Closed.Contains(neighbour)) continue;
                int tempGCost = currentNode.GCost + CalculateDistanceCost(currentNode, neighbour);

                if (tempGCost < neighbour.GCost)
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
        var currentNode = endNode;
        List<PathNode> path = new List<PathNode>();
        while (currentNode.fromNode != null)
        {
            path.Prepend(currentNode);
            currentNode = currentNode.fromNode;
        }
        return path;
    }


    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return 14 * Mathf.Min(xDistance, yDistance) + 10 * remaining;
    }


}
