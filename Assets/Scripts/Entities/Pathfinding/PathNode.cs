using System.Collections.Generic;
using UnityEngine;
public class PathNode
{
    public int x;
    public int y;
    CustomGrid<PathNode> grid;
    public bool isWalkable;

    public PathNode fromNode { get; private set; } //which node this one was traversed from (used for returning the full path)
    public int GCost {  get; private set; } //distance from the start node
    public int HCost { get; private set; } //distance from the target node (optimistic)

    public int FCost => GCost + HCost; //the lower this value is, the more attractive this node becomes



    public PathNode(CustomGrid<PathNode> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        isWalkable = true; //all nodes should be walkable by default
    }

    public void SetGCost(int newG) => GCost = newG;
    public void SetHCost(int newH) => HCost = newH;
    public void SetFromNode(PathNode newFromNode) => fromNode = newFromNode;


    public List<PathNode> GetNeighbours()
    {
        var neighbours = new List<PathNode>();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                var current = grid.GetCellValue(x - 1 + i, y - 1 + j);//reference frame of [x-1,y-1] to [x+1,y+1]
                if (current != default(PathNode)) neighbours.Add(current); //GetCellValue returns default(TGridObject) if the index is not within bounds of grid
            }
        }
        neighbours.Remove(grid.GetCellValue(x, y)); //this is horrible, but likely better than having another conditional on each loop iteration
        return neighbours;
    }

    public Vector3 GetWorldPos()
    {
        return grid.WorldPosFromIndex(this.x, this.y);
    }

    public override string ToString()
    {
        return "(x: " + x + " y:" + y + ")";
    }
}
