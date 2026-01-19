using UnityEngine;

public abstract class AbstractOccupierFactory : MonoBehaviour
{
    public abstract IOccupier CreateOccupier();
}
