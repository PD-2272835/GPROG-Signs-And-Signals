using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GooberContext : MonoBehaviour, ISelectable
{
    //State Machine
    private GooberState _currentState;

    //State Instances
    public GooberIdle Idle = new GooberIdle();
    public GooberPathing Pathing = new GooberPathing();
    public GooberWorking Working = new GooberWorking();
    public GooberHunting Hunting = new GooberHunting();
    public GooberAfraid Afraid = new GooberAfraid();


    [SerializeField] public float movementSpeed = 1f; //{ get; private set; }
    [SerializeField] private SpriteRenderer _selectionOutline = null;
    [SerializeField] private bool _isSelected = false;
    public Vector3 targetPosition;
    
    public float wanderChance = 0.02f;
    public float maxWanderWaitPeriod = 10f;
    public int maxWanderDistance = 3;



    public void Start()
    {
        _currentState = Idle;
        _currentState.EnterState(this);

        if (_selectionOutline == null)
        {
            _selectionOutline = transform.Find("SelectionOutline").gameObject.GetComponent<SpriteRenderer>();
            if (_selectionOutline == null ) throw new System.Exception("No Outline Object or Renderer found, cannot implement ISelectable");
        }
        _selectionOutline.enabled = _isSelected;
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
        this.targetPosition = targetPosition;
        ChangeState(Pathing);
    }

    public void UpdatePosition(Vector3 newPosition)
    {
        transform.position += newPosition;
    }

    //ISelectable Setters concrete implementation
    public void SetSelected(bool newSelection)
    {
        _isSelected = newSelection;
        _selectionOutline.enabled = _isSelected;
    }
    public void ToggleSelected()
    {
        _isSelected = !_isSelected;
        _selectionOutline.enabled = _isSelected;
    }


    //Getters
    public Vector3 GetPosition()
    {
        return transform.position;
    }

}
