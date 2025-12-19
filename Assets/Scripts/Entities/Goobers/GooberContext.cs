using UnityEngine;

public class GooberContext : MonoBehaviour
{
    //State Machine
    private GooberState _currentState;
    private GooberState _previousState;

    //State Instances
    public GooberIdle Idle = new GooberIdle();
    public GooberPathing Pathing = new GooberPathing();
    public GooberWorking Working = new GooberWorking();
    public GooberHunting Hunting = new GooberHunting();
    public GooberAfraid Afraid = new GooberAfraid();


    bool isSelected = false;


    public void Start()
    {
        _currentState = Idle;
        _previousState = Idle;
        _currentState.EnterState(this);
    }

    //callback to currentstate's update function
    public void Update()
    {
        _currentState?.Update(this);
    }




    //State Transition
    public void ChangeState(GooberState newState)
    {
        _currentState?.ExitState(this);
        _previousState = _currentState;
        _currentState = newState;
        _currentState?.EnterState(this);
    }

}
