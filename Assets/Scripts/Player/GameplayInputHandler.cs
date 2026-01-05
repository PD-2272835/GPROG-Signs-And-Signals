using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayInputHandler : MonoBehaviour
{
    private Camera _mainCam;

    private Vector2 _moveDirection;
    public float movementSpeed = 10.0f;
    

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
        
        var rayHit = Physics2D.GetRayIntersection(_mainCam.ScreenPointToRay(Mouse.current.position.ReadValue()));
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>().normalized;
    }
}
