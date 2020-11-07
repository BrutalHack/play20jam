using System;
using System.Collections.Generic;
using Supyrb;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData = null;
    [SerializeField] private Transform lookRotation = null;

    [SerializeField] private float _energy;
    [SerializeField] private int _water;
    private Vector2 _moveVector;
    private Vector3 forward = Vector3.forward;
    private Rigidbody _rigidbody;

    private float _surroundingEnergy;
    private float _energyChangeMultiplier = 1f;
    private bool _isAlive;
    private PlayerEnteredSafeZoneSignal _enterSafeZoneSignal;
    private PlayerExitSafeZoneSignal _exitSafeZoneSignal;
    private PlayerEnergyLevelChangedSignal _playerEnergyLevelChangedSignal;
    private PlayerDiedSignal _playerDiedSignal;
    private WaterCollectedSignal _waterCollectedSignal;
    private List<IInteractable> _interactables = new List<IInteractable>();

    public bool HasWater => _water == 1;

    private void Awake()
    {
        _surroundingEnergy = _playerData.EnergyLossInFog;

        Signals.Get(out _enterSafeZoneSignal);
        Signals.Get(out _exitSafeZoneSignal);
        Signals.Get(out _playerDiedSignal);
        Signals.Get(out _playerEnergyLevelChangedSignal);
        Signals.Get(out _waterCollectedSignal);

        _enterSafeZoneSignal.AddListener(OnEnterSafeZone);
        _exitSafeZoneSignal.AddListener(OnExitSafeZone);
        _playerEnergyLevelChangedSignal.AddListener(OnEnergyLevelChanged);
        _playerDiedSignal.AddListener(OnPlayerDied);
        _waterCollectedSignal.AddListener(OnWaterCollected);
    }

    private void OnWaterCollected()
    {
        //TODO send animation event
        _water = 1;
    }

    private void Start()
    {
        _isAlive = true;
        _energy = _playerData.MaxEnergy;
        _water = _playerData.StartWater;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnDestroy()
    {
        _enterSafeZoneSignal.RemoveListener(OnEnterSafeZone);
        _exitSafeZoneSignal.RemoveListener(OnExitSafeZone);
        _playerEnergyLevelChangedSignal.RemoveListener(OnEnergyLevelChanged);
        _playerDiedSignal.RemoveListener(OnPlayerDied);
        _waterCollectedSignal.RemoveListener(OnWaterCollected);
    }

    private void Update()
    {
        if (!_isAlive)
        {
            return;
        }

        forward.x = _moveVector.x;
        forward.z = _moveVector.y;
        if (forward.sqrMagnitude > 0.1f)
        {
            var targetRotation = Quaternion.LookRotation(forward);
            lookRotation.rotation = Quaternion.RotateTowards(lookRotation.rotation, targetRotation,
                _playerData.LightRotationSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (!_isAlive)
        {
            return;
        }

        UpdateEnergy();
        UpdateMovement();
    }

    private void UpdateEnergy()
    {
        var newEnergyLevel = Mathf.Min(_energy + _surroundingEnergy * _energyChangeMultiplier * Time.deltaTime,
            _playerData.MaxEnergy);
        if (newEnergyLevel == _energy)
        {
            return;
        }

        _playerEnergyLevelChangedSignal.Dispatch(newEnergyLevel);

        if (newEnergyLevel <= 0.0f)
        {
            _playerDiedSignal.Dispatch();
        }
    }

    private void UpdateMovement()
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
        _surroundingEnergy = hub.Data.EmittingEnergy;
        Debug.Log("Entered safe zone");
    }

    private void OnExitSafeZone(SafeZoneHub hub)
    {
        _surroundingEnergy = -_playerData.EnergyLossInFog;
        Debug.Log("Exit safe zone");
    }

    private void OnEnergyLevelChanged(float newLevel)
    {
        _energy = newLevel;

        _energyChangeMultiplier = (_energy <= _playerData.CriticalEnergyThreshold)
            ? _playerData.CriticalEnergyLossMultiplier
            : 1f;
    }

    private void OnPlayerDied()
    {
        _isAlive = false;
        _energy = 0.0f;
    }

    private void Interact()
    {
        var interactedObjects = new List<IInteractable>();
        foreach (var interactable in _interactables)
        {
            if (interactable.Interact())
            {
                interactedObjects.Add(interactable);
            }
        }

        _interactables.RemoveAll(interactedObjects.Contains);
    }

    public void AddInteractable(IInteractable otherInteractable)
    {
        _interactables.Add(otherInteractable);
    }

    public void RemoveInteractable(IInteractable otherInteractable)
    {
        if (!_interactables.Remove(otherInteractable))
        {
            Debug.Log($"Tried to remove {otherInteractable}, but was not registered.");
        }
    }

    public void RemoveWater()
    {
        _water = 0;
    }
}