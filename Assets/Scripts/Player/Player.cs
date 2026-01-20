using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    private Camera _MainCam;

    private Vector2 _MoveDirection;
    public float MovementSpeed = 10.0f;
    [SerializeField] private bool _IsBuilding = false;

    private List<GameObject> _Selection;

    public void Awake()
    {
        _Selection = new List<GameObject>();
        _MainCam = Camera.main;
    }

    private void Update()
    {
        Vector2 modifiedMovementDirection = _MoveDirection * MovementSpeed * Time.deltaTime;
        _MainCam.transform.position += new Vector3(modifiedMovementDirection.x, modifiedMovementDirection.y, 0);
    }


    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        
        RaycastHit2D rayHit = Physics2D.Raycast(_MainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector3.forward, Mathf.Infinity);


        if (rayHit)
        {
            GameObject hitObject = rayHit.transform.gameObject;
            TrySetObjectSelection(hitObject, true);
            
            if (_Selection.Count >= 2)
            {
                PerformActionBasedOnSelection();
                _Selection.Clear();
            }
        }
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        _MoveDirection = context.ReadValue<Vector2>().normalized;
    }

    private void TrySetObjectSelection(GameObject item, bool selectionState)
    {
        if (item && item.TryGetComponent(out ISelectable selectable))
        {
            selectable.SetSelected(selectionState);
            _Selection.Add(item);
        }
    }


    //this method assumes that Selection has a length greater than 2
    private void PerformActionBasedOnSelection()
    {
        //if (isBuilding)
        if (_Selection[0].TryGetComponent(out GooberContext goober) && _Selection[1])
        {
            goober.PathTo(_Selection[1].transform.position);
        }


        //deselect objects, only do once if the same
        if (_Selection[0] == _Selection[1])
            TrySetObjectSelection(_Selection[0], false); 
        else
        {
            TrySetObjectSelection(_Selection[0], false);
            TrySetObjectSelection(_Selection[1], false);
        } 
    }
}
