// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SafeZoneTrigger.cs">
//   Copyright (c) 2020 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using Supyrb;
using UnityEngine;

public class SafeZoneTrigger: MonoBehaviour
{
	[SerializeField]
	private LayerMask playerLayerMask = new LayerMask();
    
	private SafeZoneHub parent;
	private SphereCollider sphereCollider;
	private bool playerInside;

	private PlayerEnteredSafeZoneSignal playerEnteredSafeZoneSignal;
	private PlayerExitSafeZoneSignal playerExitSafeZoneSignal;
    

	public void Initialize(SafeZoneHub safeZoneHub)
	{
		sphereCollider = GetComponent<SphereCollider>();
		parent = safeZoneHub;

		Signals.Get(out playerEnteredSafeZoneSignal);
		Signals.Get(out playerExitSafeZoneSignal);
	}

	private void OnTriggerEnter(Collider other)
	{
		playerEnteredSafeZoneSignal.Dispatch(parent);
		playerInside = true;
	}
    
	private void OnTriggerExit(Collider other)
	{
		playerExitSafeZoneSignal.Dispatch(parent);
		playerInside = false;
	}

	public void SetRadius(float radius)
	{
		sphereCollider.radius = radius;
	}

	public void SetActive(bool active)
	{
		if (active)
		{
			SphereCastForPlayer();
		}
		else
		{
			if (playerInside)
			{
				playerExitSafeZoneSignal.Dispatch(parent);
				playerInside = false;
			}
		}
		sphereCollider.enabled = active;
        
	}

	private void SphereCastForPlayer()
	{
		var inside = Physics.CheckSphere(transform.position, sphereCollider.radius, playerLayerMask);
		if (inside)
		{
			playerEnteredSafeZoneSignal.Dispatch(parent);
			playerInside = true;
		}
	}
}