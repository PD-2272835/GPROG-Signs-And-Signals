using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public int XPos;
    public int YPos;
    private CustomGrid<PathNode> _Grid;

    public bool IsWalkable;

    public PathNode FromNode { get; private set; } //which node this one was traversed from (used for returning the full path)
    public int GCost {  get; private set; } //distance from the start node
    public int HCost { get; private set; } //distance from the target node (optimistic)

    public int FCost => GCost + HCost; //the lower this value is, the more attractive this node becomes



    public PathNode(CustomGrid<PathNode> grid, int x, int y)
    {
        _Grid = grid;
        XPos = x;
        YPos = y;
        IsWalkable = true; //all nodes should be walkable by default
    }

    public void SetGCost(int newG) => GCost = newG;
    public void SetHCost(int newH) => HCost = newH;
    public void SetFromNode(PathNode newFromNode) => FromNode = newFromNode;


    public List<PathNode> GetNeighbours()
    {
        var neighbours = new List<PathNode>();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                var current = _Grid.GetCellValue(XPos - 1 + i, YPos - 1 + j);//reference frame of [x-1,y-1] to [x+1,y+1]
                if (current != default(PathNode) && current != null) neighbours.Add(current); //GetCellValue returns default(TGridObject) if the index is not within bounds of grid
            }
        }
        neighbours.Remove(_Grid.GetCellValue(XPos, YPos)); //this is horrible, but likely better than having another conditional on each loop iteration
        return neighbours;
    }

    public Vector3 GetWorldPos()
    {
        return _Grid.WorldPosFromIndex(XPos, YPos);
    }

    public override string ToString()
    {
        return "(x: " + XPos + " y:" + YPos + ")";
    }
}
