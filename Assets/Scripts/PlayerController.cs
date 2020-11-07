using System;
using Supyrb;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData = null;

    private float _energy;
    private float _water;
    private Vector2 _moveVector;
    private Rigidbody _rigidbody;

    private PlayerEnteredSafeZoneSignal _enterSafeZoneSignal;
    private PlayerEnteredSafeZoneSignal _exitSafeZoneSignal;

    private void Awake()
    {
        Signals.Get(out _enterSafeZoneSignal);
        Signals.Get(out _exitSafeZoneSignal);
        
    }

    private void Start()
    {
        _energy = _playerData.StartEnergy;
        _water = _playerData.StartWater;
        _rigidbody = GetComponent<Rigidbody>();

        _enterSafeZoneSignal.AddListener(OnEnterSafeZone);
        _exitSafeZoneSignal.AddListener(OnExitSafeZone);
    }

    private void FixedUpdate()
    {
        var moveDelta = _moveVector * (_playerData.MovementSpeed * Time.deltaTime);
        _rigidbody.velocity += new Vector3(moveDelta.x, 0, moveDelta.y);
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

        Interact();
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
    
    
    private void OnEnterSafeZone(SafeZoneHub hub)
    {
        Debug.Log("Entered safe zone");
    }

    private void OnExitSafeZone(SafeZoneHub hub)
    {
        Debug.Log("Exit safe zone");
    }
    
    private void Interact()
    {
        
    }
}