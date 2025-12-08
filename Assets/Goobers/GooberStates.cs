using UnityEngine;


//callbacks that each state must implement
/*public interface IGooberState
{
    void HandleInput();
    void Update();
}
*/

public abstract class GooberState
{

    public abstract void HandleInput();
    public abstract void Update();
}


