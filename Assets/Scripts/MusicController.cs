// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MusicController.cs">
//   Copyright (c) 2020 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using System;
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
	private VictorySignal victorySignal;
	private GameSceneSignal gameSceneSignal;
	private MenuSceneSignal menuSceneSignal;
	
	private void Awake()
	{
		eventEmitter = GetComponent<StudioEventEmitter>();
        
		Signals.Get(out playerEnergyLevelChangedSignal);
		Signals.Get(out victorySignal);
		Signals.Get(out gameSceneSignal);
		Signals.Get(out menuSceneSignal);

		playerEnergyLevelChangedSignal.AddListener(OnEnergyLevelChanged);
		victorySignal.AddListener(OnVictory);
		gameSceneSignal.AddListener(OnSceneReload);
		menuSceneSignal.AddListener(OnSceneReload);
	}

	void Start()
	{
		eventEmitter.Play();
	}

	private void OnDestroy()
	{
		playerEnergyLevelChangedSignal.RemoveListener(OnEnergyLevelChanged);
		victorySignal.RemoveListener(OnVictory);
		gameSceneSignal.RemoveListener(OnSceneReload);
		menuSceneSignal.RemoveListener(OnSceneReload);
	}

	private void OnEnergyLevelChanged(float level)
	{
		var intensity = energyIntensityRemapper.Evaluate(level);
		eventEmitter.SetParameter("Intensity", intensity);
	}
	
	private void OnVictory()
	{
		eventEmitter.SetParameter("Win", 1);
	}
	
	private void OnSceneReload()
	{
		eventEmitter.SetParameter("Win", 0);
		eventEmitter.SetParameter("Intensity", 1);
	}
}