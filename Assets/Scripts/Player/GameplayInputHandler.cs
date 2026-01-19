using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;

public class GameplayInputHandler : MonoBehaviour
{
    private Camera _mainCam;

    private Vector2 _moveDirection;
    public float movementSpeed = 10.0f;

    private List<GameObject> Selection;

    public void Awake()
    {
        _mainCam = Camera.main;
    }

    private void Update()
    {
        Vector2 modifiedMovementDirection = _moveDirection * movementSpeed * Time.deltaTime;
        _mainCam.transform.position += new Vector3(modifiedMovementDirection.x, modifiedMovementDirection.y, 0);
    }


    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        
        RaycastHit2D rayHit = Physics2D.Raycast(_mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector3.forward, Mathf.Infinity);


        if (rayHit)
        {
            GameObject hitObject = rayHit.transform.gameObject;
            TrySetObjectSelection(hitObject, true);
            
            if (Selection.Count >= 2)
            {
                PerformActionBasedOnSelection();
                Selection.Clear();
            }
        }
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>().normalized;
    }

    private void TrySetObjectSelection(GameObject item, bool selectionState)
    {
        if (item && item.TryGetComponent(out ISelectable selectable))
        {
            selectable.SetSelected(selectionState);
            Selection.Add(item);
        }
    }


    //this method assumes that Selection has a length greater than 2
    private void PerformActionBasedOnSelection()
    {
        if (Selection[0].TryGetComponent(out GooberContext goober) && Selection[1])
        {
            goober.PathTo(Selection[1].transform.position);
        }



        if (Selection[0] == Selection[1])
            TrySetObjectSelection(Selection[0], false); //if the selected object is the same, do nothing and deselect object as no action should be performed
        else
        {
            TrySetObjectSelection(Selection[0], false);
            TrySetObjectSelection(Selection[1], false);
        } 
    }
}
