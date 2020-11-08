﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerAnimationEvents.cs">
//   Copyright (c) 2020 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using FMODUnity;
using UnityEngine;

public class PlayerAnimationEvents: MonoBehaviour
{
	[SerializeField]
	private StudioEventEmitter footStepSound = null;

	public void PlayFootStepSound()
	{
		footStepSound.Play();
	}
}