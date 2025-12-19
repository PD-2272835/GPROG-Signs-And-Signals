using UnityEngine;

public class PathNode
{
    public int GCost {  get; private set; }
    public int HCost { get; private set; }
    public int FCost => GCost + HCost;
    
    public PathNode FromNode;


    public PathNode()
    {

    }


}
