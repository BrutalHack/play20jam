// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerData.cs">
//   Copyright (c) 2020 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

[CreateAssetMenu(menuName = "Play2020/PlayerData", fileName = "PlayerData", order = 0)]
public class PlayerData: ScriptableObject
{
	[SerializeField]
	private float movementSpeed = 50;

	[SerializeField]
	private float lightRotationSpeed = 360f;

	[SerializeField]
	private float maxEnergy = 1f;

	[SerializeField]
	private float maxWater = 5f;
	
	[SerializeField]
	private int startWater = 0;

	[SerializeField]
	private float energyLossInFog = 0.05f;

	[SerializeField, Range(0f, 1f)]
	private float criticalEnergyThreshold = 0.3f;

	[SerializeField, Range(0f, 1f)]
	private float criticalEnergyLossMultiplier = 0.7f;
	
	public float MovementSpeed => movementSpeed;

	public float LightRotationSpeed => lightRotationSpeed;

	public float MaxEnergy => maxEnergy;

	public float MaxWater => maxWater;

	public int StartWater => startWater;

	public float EnergyLossInFog => energyLossInFog;

	public float CriticalEnergyThreshold => criticalEnergyThreshold;

	public float CriticalEnergyLossMultiplier => criticalEnergyLossMultiplier;
}