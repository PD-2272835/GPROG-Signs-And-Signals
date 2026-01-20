using UnityEngine;

public class Tile : MonoBehaviour
{
    private TerrainType _terrainType;
    //private Occupier _occupier;  //(for buildings/trees etc)
    private CustomGrid<Tile> _grid;
    public int x;
    public int y;

    public Tile(CustomGrid<Tile> grid, int x, int y)
    {
        _grid = grid;
        this.x = x;
        this.y = y;
    }

    public void SetTerrainType(TerrainType type)
    {
        _terrainType = type;
    }

    public void SetStructure()
    {
        
    }
}