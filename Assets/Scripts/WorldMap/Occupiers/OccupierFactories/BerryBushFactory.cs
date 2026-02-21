using UnityEngine;

public class BerryBushFactory : OccupierFactory
{
    [SerializeField] private BerryBush _Prefab;

    public override Occupier CreateOccupier(Vector3 position)
    {
        return CreateOccupier(position, _Prefab);
    }
}
