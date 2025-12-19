using UnityEngine;

[CreateAssetMenu(fileName = "Terrain Type", menuName = "Scriptable Objects/TerrainType")]
public class TerrainType : ScriptableObject
{
    public Color _tileColor { get; private set; }
    public bool _isWater { get; private set; }

    public float _mapElevation { get; private set; }
    //public float _biomeTemperature { get; private set; }
}
