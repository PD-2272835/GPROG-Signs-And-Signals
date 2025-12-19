using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    public PathNode StartNode;
    public PathNode EndNode;

    CustomGrid<PathNode> Grid;
    public List<PathNode> Open;
    public List<PathNode> Closed;


    public Pathfinding(int width, int height)
    {
        Grid = new CustomGrid<PathNode>(width, height, );
    }


    public List<PathNode> FindPath()
    {

    }

}
