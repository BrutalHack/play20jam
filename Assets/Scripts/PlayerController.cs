using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector2 _moveVector;
    [SerializeField] private float _moveSpeed;
    private Rigidbody _rigidbody;

    private void Update()
    {
        var moveDelta = _moveVector * (_moveSpeed * Time.deltaTime);
        _rigidbody.velocity += new Vector3(moveDelta.x, 0, moveDelta.y);
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
