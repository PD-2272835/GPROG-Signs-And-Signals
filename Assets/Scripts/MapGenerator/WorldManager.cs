using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(AbstractOccupierFactory))]
public class WorldManager : MonoBehaviour
{
    //this should be a singleton attached to a GameObject to hold the World, accessible through this static Instance
    public static WorldManager Instance { get; private set; }

    //Map/Pathfinding Creation Parameters
    [SerializeField] private int _MapWidth;
    [SerializeField] private int _MapHeight;
    [SerializeField] private float _CellSize = 1f;

    private Pathfinding _PathfindingInstance;
    private CustomGrid<Occupier> _Occupiers;


    //Proc-gen Parameters
    [SerializeField] private float _OccupierPlacementThreshold = 0.5f;
    [SerializeField] private float _NoiseFrequency = 1.0f;
    [SerializeField] private float _NoiseOffsetX;
    [SerializeField] private float _NoiseOffsetY;

    private AbstractOccupierFactory[] _OccupierFactories;



    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;

        if (Pathfinding.Instance == null) _PathfindingInstance = new Pathfinding(_MapWidth, _MapHeight, _CellSize, transform.position);

        _Occupiers = new CustomGrid<Occupier>(_MapWidth, _MapHeight, _CellSize, transform.position);

        _OccupierFactories = gameObject.GetComponents<AbstractOccupierFactory>();

        _NoiseOffsetX = Random.Range(0f, 100f);
        _NoiseOffsetY = Random.Range(0, 100f);

        GenerateMap();
    }


    //hyper-simplified world gen using high frequency perlin noise - "blue noise"
    private void GenerateMap()
    {
        Debug.Log("entered generate map");
        for (float x = 0.0f; x < _MapWidth; x++)
        {
            for (float y = 0.0f; y < _MapHeight; y++)
            {
                float xCoord = x / _MapWidth;
                float yCoord = y / _MapHeight;
                float perlinValue = Mathf.PerlinNoise(_NoiseFrequency * xCoord - _NoiseOffsetX, _NoiseFrequency * yCoord - _NoiseOffsetY);
                Debug.Log(perlinValue);
                if (perlinValue > _OccupierPlacementThreshold)
                {
                    Debug.Log("tried to place occupier");
                    PlaceOccupier(_OccupierFactories[Random.Range(0, _OccupierFactories.Length)], (int)x, (int)y);
                }
            }
        }
    }



    public void PlaceOccupier(AbstractOccupierFactory occupierFactory, int x, int y)
    {
        Occupier occupier = occupierFactory.CreateOccupier(_Occupiers.WorldPosFromIndex(x, y));
        _Occupiers.SetCellValue(occupier, x, y);
        Pathfinding.Instance.Grid.GetCellValue(x, y).IsWalkable = false;
    }

    public Occupier GetOccupierFromWorldPos(Vector3 worldPos)
    {
        return _Occupiers.GetCellValue(worldPos);
    }


    private void OnDrawGizmosSelected()
    {
        if (_PathfindingInstance != null)
        {
            Pathfinding.Instance.Grid.DebugDrawGrid();
        }
    }
}
