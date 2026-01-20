using UnityEngine;

public class BerryBushFactory : AbstractOccupierFactory
{
    [SerializeField] private BerryBush _Prefab;

    public override Occupier CreateOccupier(Vector3 position)
    {
        BerryBush newOccupier = Instantiate(_Prefab, position, Quaternion.identity);
        newOccupier.Initialize();
        return newOccupier;
    }
}
