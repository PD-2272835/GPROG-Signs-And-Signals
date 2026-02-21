using UnityEngine;


[RequireComponent(typeof(OccupierFactory))]
public class WorldManager : MonoBehaviour
{
    //this should be a singleton attached to a GameObject to hold the World, accessible through this static Instance
    private static WorldManager _Instance;
    public static WorldManager GetInstance()
    {  return _Instance; }

    //Map/Pathfinding Creation Parameters
    [SerializeField] private int _MapWidth;
    [SerializeField] private int _MapHeight;
    [SerializeField] private float _CellSize = 1f;

    private OccupierFactory[] _OccupierFactories;

    private Pathfinding _PathfindingInstance;
    private CustomGrid<Occupier> _OccupierGrid;


    //Proc-gen Parameters
    [SerializeField] private float _OccupierPlacementThreshold = 0.5f;
    [SerializeField] private float _NoiseFrequency = 1.0f;
    [SerializeField] private float _NoiseOffsetX;
    [SerializeField] private float _NoiseOffsetY;



    private void Awake()
    {
        //ensure this class is a singleton
        if (_Instance != null && _Instance != this) Destroy(this);
        else _Instance = this;

        if (Pathfinding.GetInstance() == null) _PathfindingInstance = new Pathfinding(_MapWidth, _MapHeight, _CellSize, transform.position);

        _OccupierGrid = new CustomGrid<Occupier>(_MapWidth, _MapHeight, _CellSize, transform.position);

        _OccupierFactories = gameObject.GetComponents<OccupierFactory>();

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
                
                //if the perlin noise is above a placement threshhold value, place a random occupier from the _Occupiers list
                if (perlinValue > _OccupierPlacementThreshold)
                {
                    PlaceOccupier(_OccupierFactories[Random.Range(0, _OccupierFactories.Length)], (int)x, (int)y);
                }
            }
        }
    }



    public void PlaceOccupier(OccupierFactory occupierFactory, int x, int y)
    {
        Occupier occupierInstance = occupierFactory.CreateOccupier(_OccupierGrid.WorldPosFromIndex(x, y));
        _OccupierGrid.SetCellValue(occupierInstance, x, y);
        Pathfinding.GetInstance().Grid.GetCellValue(x, y).IsWalkable = false;
    }

    public Occupier GetOccupierFromWorldPos(Vector3 worldPos)
    {
        return _OccupierGrid.GetCellValue(worldPos);
    }


    private void OnDrawGizmosSelected()
    {
        if (_PathfindingInstance != null)
        {
            Pathfinding.GetInstance().Grid.DebugDrawGrid();
        }
    }
}
