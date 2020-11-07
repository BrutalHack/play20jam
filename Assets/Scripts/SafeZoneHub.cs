// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Hub.cs">
//   Copyright (c) 2020 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Supyrb;
using UnityEngine;

public class SafeZoneHub : MonoBehaviour, IInteractable
{
    [SerializeField] private ParticleSystemForceField forceField = null;

    [SerializeField] private SafeZoneTrigger safeZoneTrigger = null;

    [SerializeField] private SafeZoneData data = null;

    [SerializeField] private Animator animator = null;

    [SerializeField] private ShaderParameter activeParam = null; 
    
    [SerializeField] private bool activated = false;

    public SafeZoneData Data => data;

    private float energyLeft;
    private PlayerController _playerController;

    private SafeZoneActivatedSignal _safeZoneActivatedSignal;

    public InteractionType Type
    {
        get { return InteractionType.WaterIt; }
    }

    private void Awake()
    {
        _playerController = GameObject.FindWithTag(UnityTags.Player).GetComponent<PlayerController>();
        forceField.endRange = data.Size;
        safeZoneTrigger.Initialize(this);
        safeZoneTrigger.SetRadius(data.Size);
        energyLeft = data.StartEnergy;

        Signals.Get(out _safeZoneActivatedSignal);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(UnityTags.Player))
        {
            _playerController.AddInteractable(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(UnityTags.Player))
        {
            _playerController.RemoveInteractable(this);
        }
    }

    private void Start()
    {
        SetHubActive(activated);
    }

    private void SetHubActive(bool active)
    {
        forceField.gameObject.SetActive(active);
        safeZoneTrigger.SetActive(active);
        animator.SetBool(activeParam.Name, active);
    }

    public bool Interact()
    {
        if (_playerController == null || activated)
        {
            return false;
        }

        if (_playerController.HasWater)
        {
            _playerController.RemoveWater();
            _safeZoneActivatedSignal.Dispatch();
            SetHubActive(true);
            return true;
        }
        else
        {
            //TODO No Water?
            return false;
        }
    }
}