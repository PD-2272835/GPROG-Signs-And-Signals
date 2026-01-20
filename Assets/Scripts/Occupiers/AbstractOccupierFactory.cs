using UnityEngine;

public abstract class AbstractOccupierFactory : MonoBehaviour
{
    public abstract Occupier CreateOccupier(Vector3 position);
}
