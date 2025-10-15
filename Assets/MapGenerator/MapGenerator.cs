using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //this should be a singleton attached to a GameObject to hold the map
    //(not sure if singleton pattern should be used here)
    public static MapGenerator instance {  get; private set; }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }


    [SerializeField] private int _mapWidth;
    [SerializeField] private int _mapHeight;

    [SerializeField] private Tile _gridTile;

    [SerializeField] private TerrainType[] terrainTypes;

    private Dictionary<Vector2, Tile> _grid;

    public void CreateNewGrid()
    {
         _grid = new Dictionary<Vector2, Tile>();

        for (int y = 0; y < _mapHeight; y++)
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                //looks suspicious, not sure if assigning the correct thing...
                _grid[new Vector2(x, y)] = _gridTile;

            }
        }
    }

    public Tile GetTileAtPosition(Vector2 position)
    {
        if(_grid.TryGetValue(position, out Tile tile)) return tile;
        else return null;
    }
}
