using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinding
{   
    const int DIAGONAL_MOVE_COST = 14;
    const int CARDINAL_MOVE_COST = 10;

    private static Pathfinding _Instance;
    public static Pathfinding GetInstance()
    {  return _Instance; }


    public CustomGrid<PathNode> Grid;
    private List<PathNode> _Open;
    private List<PathNode> _Closed;


    public Pathfinding(int width, int height, float cellSize, Vector3 originPosition)
    {
        if (_Instance == null) _Instance = this;

        //initialize grid values with a reference to this grid, and the x and y pos of each node
        Grid = new CustomGrid<PathNode>(width, height, cellSize, originPosition, (CustomGrid<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }


    public List<Vector3> FindPath(Vector3 startWorldPosition, Vector3 targetWorldPosition)
    {
        PathNode startNode = Grid.GetCellValue(startWorldPosition);
        PathNode targetNode = Grid.GetCellValue(targetWorldPosition);
        List<PathNode> path = FindPath(startNode.XPos, startNode.YPos, targetNode.XPos, targetNode.YPos);

        if (path == null) return null;
        else
        {
            List<Vector3> result = new List<Vector3>();
            foreach (PathNode node in path)
            {
                result.Add(node.GetWorldPos());
            }

            return result;
        }
    }


    public List<PathNode> FindPath(int startNodeX, int startNodeY, int targetPosX, int targetPosY)
    {
        PathNode startNode = Grid.GetCellValue(startNodeX, startNodeY);
        PathNode targetNode = Grid.GetCellValue(targetPosX, targetPosY);

        _Open = new List<PathNode>() { startNode };
        _Closed = new List<PathNode>();

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


        while (_Open.Any())
        {
            PathNode currentNode = ChooseNodeToExplore();

            if (currentNode == targetNode) return ReconstructPath(targetNode); //if the path has been found, return it;
                

            _Closed.Add(currentNode);
            _Open.Remove(currentNode);
            

            foreach (var neighbour in currentNode.GetNeighbours())
            {
                if (_Closed.Contains(neighbour)) continue; //ensure the neighbour has not already been considered (still possible to move from closed to open if another path to it is found)
                if (!neighbour.IsWalkable && neighbour != targetNode) //ensure the neighbour can be traversed
                {
                    _Closed.Add(neighbour);
                    continue;
                }

                int tempGCost = currentNode.GCost + CalculateDistanceCost(currentNode, neighbour); //calculate "tentative cost"

                if (tempGCost < neighbour.GCost) //select the node with the lowest Gcost
                {
                    neighbour.SetFromNode(currentNode);
                    neighbour.SetGCost(tempGCost);
                    neighbour.SetHCost(CalculateDistanceCost(neighbour, targetNode));

                    if (!_Open.Contains(neighbour))
                    {
                        _Open.Add(neighbour);
                    }
                }
            }

        }

        //Entire grid was searched, no path found
        return null;
    }


    
    private PathNode ChooseNodeToExplore() //evaluate which node to explore
    {
        var current = _Open[0];
        foreach (var other in _Open)
        {
            if (other.FCost < current.FCost || (other.FCost == current.FCost && other.HCost < current.HCost)) //choose either Node with lowest Fcost, if there are multiple of same Fcost, node with both lowest F and H cost
            {
                current = other;
            }
        }
        return current;
    }


    private List<PathNode> ReconstructPath(PathNode endNode) //return the found path
    {
        PathNode currentNode = endNode;
        List<PathNode> path = new List<PathNode>();
        while (currentNode != null)
        {
            path.Add(currentNode);
            currentNode = currentNode.FromNode;
        }
        path.Reverse();
        return path;
    }


    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.XPos - b.XPos);
        int yDistance = Mathf.Abs(a.YPos - b.YPos);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return DIAGONAL_MOVE_COST * Mathf.Min(xDistance, yDistance) + CARDINAL_MOVE_COST * remaining;
    }
}
