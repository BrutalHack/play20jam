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
using UnityEngine;

public class SafeZoneHub: MonoBehaviour
{
    [SerializeField]
    private ParticleSystemForceField forceField = null;

    [SerializeField]
    private SafeZoneTrigger safeZoneTrigger = null;
    
    [SerializeField]
    private SafeZoneData data = null;

    [SerializeField]
    private bool activated = false;

    public SafeZoneData Data => data;

    private float engeryLeft;

    private void Awake()
    {
        forceField.endRange = data.Size;
        safeZoneTrigger.Initialize(this);
        safeZoneTrigger.SetRadius(data.Size);
        engeryLeft = data.StartEnergy;
    }

    private void Start()
    {
        SetHubActive(activated);
    }

    private void SetHubActive(bool active)
    {
        forceField.gameObject.SetActive(active);
        safeZoneTrigger.SetActive(active);
    }
}