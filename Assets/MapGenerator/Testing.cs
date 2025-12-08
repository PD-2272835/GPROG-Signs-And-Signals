using UnityEngine;
using UnityEngine.InputSystem;

public class Testing : MonoBehaviour
{
    public CustomGrid<bool> balls;
    [SerializeField] private int _gridWidth = 10, _gridHeight = 10;
    InputAction action;


    void Start()
    {
        balls = new CustomGrid<bool>(_gridWidth, _gridHeight, 1f, transform.position);
        action = InputSystem.actions.FindAction("Interact");
    }
   
    void Update()
    {
        if (action.IsPressed())
        {
            balls.DebugDrawGrid();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (balls != null)
        {
            balls.DebugDrawGrid();
        }
    }
}
