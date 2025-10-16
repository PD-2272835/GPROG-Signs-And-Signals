using UnityEngine;

public class Tile : MonoBehaviour
{
    private TerrainType _terrainType;
    //private Occupier _occupier;  (for buildings/trees etc)

    void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().color = _terrainType._tileColor;
    }

    public void SetTerrainType(TerrainType type)
    {
        _terrainType = type;
    }


}