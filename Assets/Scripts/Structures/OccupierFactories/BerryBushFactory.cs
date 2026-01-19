using UnityEngine;

public class BerryBushFactory : AbstractOccupierFactory
{
    public BerryBush OccupierPrefab;

    public override IOccupier CreateOccupier()
    {
        BerryBush newOccupier = Instantiate(OccupierPrefab);
        return newOccupier;
    }
}
