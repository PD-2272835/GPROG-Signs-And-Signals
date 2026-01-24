using UnityEngine;


public abstract class OccupierFactory : MonoBehaviour
{
    //derived concrete classes can implement their own logic if required
    protected virtual Occupier CreateOccupier(Vector3 position, Occupier prefab)
    {
        Occupier prefabInstance = Instantiate(prefab, position, Quaternion.identity);
        prefabInstance.Initialize();
        return prefabInstance;
    }

    //all concrete factories must implement this method template, as this is the creation method the client will use
    //generally this should be a returned call to the above method
    public abstract Occupier CreateOccupier(Vector3 position);
}
