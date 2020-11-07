using System;
using System.Collections.Generic;
using Supyrb;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData = null;
    [SerializeField] private Transform lookRotation = null;

    [SerializeField] private Animator _anim = null;
    [SerializeField] private ShaderParameter _animWalking = null;
    [SerializeField] private ShaderParameter _animHasWater = null;
    [SerializeField] private ShaderParameter _animStart = null;
    [SerializeField] private ShaderParameter _animDie = null;
    [SerializeField] private ShaderParameter _animPickup = null;
    [SerializeField] private ShaderParameter _animUseWater = null;

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
        _anim.SetBool(_animHasWater.Name, true);
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

        var isWalking = forward.sqrMagnitude > 0.05f;
        
        // TODO Why is the hash not working???
        _anim.SetBool(_animWalking.Name, isWalking);
        
        if (isWalking)
        {
            // Flip if appropriate
            var scale = _anim.transform.localScale;
            scale.x = Mathf.Sign(forward.x + scale.x * 0.01f);
            _anim.transform.localScale = scale;
            
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
        _anim.SetTrigger(_animDie.Name);
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
                switch (interactable.Type)
                {
                    case InteractionType.PickUp:
                        _anim.SetTrigger(_animPickup.Name);
                        break;
                    case InteractionType.WaterIt:
                        _anim.SetTrigger(_animUseWater.Name);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
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
        _anim.SetBool(_animHasWater.Name, false);
        _water = 0;
    }
}