using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GooberContext : MonoBehaviour, ISelectable
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
    [SerializeField] private SpriteRenderer _selectionOutline;

    public bool isSelected = false;




    public void Start()
    {
        _currentState = Idle;
        _currentState.EnterState(this);

        _selectionOutline = transform.Find("outline").gameObject.GetComponent<SpriteRenderer>();
    }

    
    public void Update()
    {
        _currentState?.Update(this); //callback to currentstate's update function
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

    //ISelectable Setters concrete implementation
    public void SetSelected(bool newSelection)
    {
        isSelected = newSelection;
        _selectionOutline.enabled = isSelected;
    }
    public void ToggleSelected()
    {
        isSelected = !isSelected;
        _selectionOutline.enabled = isSelected;
    }


    //Getters
    public Vector3 GetPosition()
    {
        return transform.position;
    }

}
