using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private TerrainType _terrainType;
    //private Occupier _occupier;  //(for buildings/trees etc)

    public void SetTerrainType(TerrainType type)
    {
        _terrainType = type;
    }

    /*public void SetOccupier(Occupier occupier)
    {
        
    }*/
}