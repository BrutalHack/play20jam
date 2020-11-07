using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector2 _moveVector;
    [SerializeField] private float _moveSpeed;

    private void Update()
    {
        if (_moveVector != Vector2.zero)
        {
            var moveDelta = _moveVector * (_moveSpeed * Time.deltaTime);
            var transformComponent = transform;
            transformComponent.position += new Vector3(moveDelta.x, 0, moveDelta.y);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            return;
        }

        _moveVector = context.ReadValue<Vector2>();
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }

        Debug.Log("Action Action Pressed");
    }

    public void OnMenu(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }

        Debug.Log("Menu Action Pressed");
    }
}
