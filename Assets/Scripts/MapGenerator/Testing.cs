using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class Testing : MonoBehaviour
{

    List<Vector3> path;
    [SerializeField] private int _gridWidth = 10, _gridHeight = 10;
    InputAction interact;
    Pathfinding pathfinding;
    [SerializeField] BerryBush bush;

    void Start()
    {
        Instantiate(bush);
        pathfinding = new Pathfinding(_gridWidth, _gridHeight);
        interact = InputSystem.actions.FindAction("Interact");
    }
   
    void Update()
    {
        if (interact.WasPressedThisFrame())
        {
            

            var mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Debug.Log(mousePos);
            
            path = Pathfinding.Instance.FindPath(Vector3.zero, mousePos);
        }
    }



    private void OnDrawGizmosSelected()
    {
        if (pathfinding != null)
        {
            Pathfinding.Instance.Grid.DebugDrawGrid();
        }

        if (path != null)
        {
            for (int i = 1; i < path.Count; i++)
            {
                var last = path[i - 1];
                var current = path[i];
                Debug.DrawLine(last, current, Color.red, 100f);
                Debug.Log(last.ToString());
                Debug.Log(current.ToString());
            }
        }
    }
}
