// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerAnimationEvents.cs">
//   Copyright (c) 2020 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using FMODUnity;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter footStepSound = null;

    [SerializeField] private StudioEventEmitter idleSound = null;

    [UsedImplicitly]
    public void PlayFootStepSound()
    {
        footStepSound.Play();
    }

    [UsedImplicitly]
    public void PlayIdleSound()
    {
        idleSound.Play();
    }

    [UsedImplicitly]
    public void ExitInteractingState()
    {
        PlayerController.Instance.ExitInteractingState();
    }
}