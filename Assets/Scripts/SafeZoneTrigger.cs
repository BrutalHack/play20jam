// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SafeZoneTrigger.cs">
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

public class SafeZoneTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayerMask;

    private SafeZoneHub _parent;
    private SphereCollider _sphereCollider;
    private bool _playerInside;

    private PlayerEnteredSafeZoneSignal _playerEnteredSafeZoneSignal;
    private PlayerExitSafeZoneSignal _playerExitSafeZoneSignal;


    public void Initialize(SafeZoneHub safeZoneHub)
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _parent = safeZoneHub;

        Signals.Get(out _playerEnteredSafeZoneSignal);
        Signals.Get(out _playerExitSafeZoneSignal);
    }

    private void OnTriggerEnter(Collider other)
    {
        _playerEnteredSafeZoneSignal.Dispatch(_parent);
        _playerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _playerExitSafeZoneSignal.Dispatch(_parent);
        _playerInside = false;
    }

    public void SetRadius(float radius)
    {
        _sphereCollider.radius = radius;
    }

    public void SetActive(bool active)
    {
        if (active)
        {
            SphereCastForPlayer();
        }
        else
        {
            if (_playerInside)
            {
                _playerExitSafeZoneSignal.Dispatch(_parent);
                _playerInside = false;
            }
        }

        _sphereCollider.enabled = active;
    }

    private void SphereCastForPlayer()
    {
        var inside = Physics.CheckSphere(transform.position, _sphereCollider.radius, playerLayerMask);
        if (inside)
        {
            _playerEnteredSafeZoneSignal.Dispatch(_parent);
            _playerInside = true;
        }
    }
}