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
	private float startEnergy = 1f;

	[SerializeField]
	private float startWater = 0f;

	public float MovementSpeed => movementSpeed;

	public float StartEnergy => startEnergy;

	public float StartWater => startWater;
}