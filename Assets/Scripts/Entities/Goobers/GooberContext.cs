using System.Collections.Generic;
using UnityEngine;

public class GooberContext : MonoBehaviour
{
    //State Machine
    private GooberState _currentState;
    private GooberState _desire;

    //State Instances
    public GooberIdle Idle = new GooberIdle();
    public GooberPathing Pathing = new GooberPathing();
    public GooberWorking Working = new GooberWorking();
    public GooberHunting Hunting = new GooberHunting();
    public GooberAfraid Afraid = new GooberAfraid();


    [SerializeField] public float movementSpeed { get; private set; }
    public bool isSelected = false;




    public void Start()
    {
        _currentState = Idle;
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
        _currentState = newState;
        _currentState?.EnterState(this);
    }


    //Setters
    public void PathTo(Vector3 targetPosition)
    {
        Pathing.SetTarget(targetPosition);
        ChangeState(Pathing);
    }

    public void UpdatePosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    //Getters

    public Vector3 GetPosition()
    {
        return transform.position;
    }

}
