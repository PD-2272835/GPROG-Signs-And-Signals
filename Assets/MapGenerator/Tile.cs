using UnityEngine;

public class Tile : MonoBehaviour
{
    private TerrainType _terrainType;
    private bool _isOnFire;
    //private Occupier _occupier;  (for buildings/trees etc)

    Tile(TerrainType type)
    {
        _terrainType = type;
    }

    void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().color = _terrainType._tileColor;
    }


}