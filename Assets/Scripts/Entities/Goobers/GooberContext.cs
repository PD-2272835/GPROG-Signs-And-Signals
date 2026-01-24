using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GooberContext : MonoBehaviour, ISelectable
{
    //State Machine
    private GooberState _CurrentState;

    //State Instances
    public GooberIdle Idle = new GooberIdle();
    public GooberPathing Pathing = new GooberPathing();
    public GooberWorking Working = new GooberWorking();
    

    //Fields used to implement ISelectable
    [SerializeField] private SpriteRenderer _SelectionOutline = null;
    [SerializeField] private bool _IsSelected = false;

    //Pathing Parameters
    public float MovementSpeed = 3f;
    public Vector3 TargetPosition;
    public float PathVariance = 0.1f;
    public float WanderChance = 0.7f;
    public float MaxWanderWaitPeriod = 4f;
    public int MaxWanderDistance = 3;

    private void Start()
    {
        _CurrentState = Idle;
        _CurrentState.EnterState(this);

        if (_SelectionOutline == null)
        {
            _SelectionOutline = transform.Find("SelectionOutline").gameObject.GetComponent<SpriteRenderer>();
            if (_SelectionOutline == null) throw new System.Exception("No Outline Object or Renderer found, cannot implement ISelectable");
        }
        _SelectionOutline.enabled = _IsSelected;
    }


    private void Update()
    {
        _CurrentState?.Update(this); //callback to currentstate's update function
    }


    //State Transition
    public void ChangeState(GooberState newState)
    {
        _CurrentState?.ExitState(this);
        _CurrentState = newState;
        _CurrentState?.EnterState(this);
    }


    public IGooberInteractable GetInteractableAtCurrentPos()
    {
        return (IGooberInteractable)WorldManager.GetInstance().GetOccupierFromWorldPos(GetPosition());
    }

    //Setters
    public void PathTo(Vector3 targetPosition)
    {
        TargetPosition = targetPosition;
        ChangeState(Pathing);
    }

    public void UpdatePosition(Vector3 newPosition)
    {
        transform.position += newPosition;
    }
    
    //ISelectable implementation
    public void SetSelected(bool newSelection)
    {
        _IsSelected = newSelection;
        _SelectionOutline.enabled = _IsSelected;
    }
    public bool GetSelected()
    {
        return _IsSelected;
    }


    //Getters
    public Vector3 GetPosition()
    {
        return transform.position;
    }

}
