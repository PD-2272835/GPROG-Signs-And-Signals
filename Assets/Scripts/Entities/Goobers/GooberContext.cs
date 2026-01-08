using System.Collections.Generic;
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


    [SerializeField] private float movementSpeed;
    public bool isSelected = false;


    private List<Vector3> _pathWaypoints;
    private int _currentWaypoint = 0;

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


    //Setters
    public void SetPath(List<Vector3> path)
    {
        _pathWaypoints = path;
        ChangeState(Pathing);
    }

    public void SetCurrentWaypoint(int newVal)
    {
        _currentWaypoint = newVal;
    }

    //Getters
    public List<Vector3> GetWaypoints()
    {
        return _pathWaypoints;
    }

    public Vector3 GetCurrentWaypoint()
    {
        return _pathWaypoints[_currentWaypoint];
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
