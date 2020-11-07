// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MusicController.cs">
//   Copyright (c) 2020 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using FMODUnity;
using Supyrb;
using UnityEngine;

public class MusicController : MonoBehaviour
{
	[SerializeField]
	private StudioEventEmitter eventEmitter = null;

	[SerializeField]
	private AnimationCurve energyIntensityRemapper = AnimationCurve.EaseInOut(0f, 4f, 1f, 0f);
    
	private PlayerEnergyLevelChangedSignal playerEnergyLevelChangedSignal;
	private void Awake()
	{
		eventEmitter = GetComponent<StudioEventEmitter>();
        
		Signals.Get(out playerEnergyLevelChangedSignal);

		playerEnergyLevelChangedSignal.AddListener(OnEnergyLevelChanged);
	}

	void Start()
	{
		eventEmitter.Play();
	}

	private void OnEnergyLevelChanged(float level)
	{
		var intensity = energyIntensityRemapper.Evaluate(level);
		eventEmitter.SetParameter("Intensity", intensity);
	}
}