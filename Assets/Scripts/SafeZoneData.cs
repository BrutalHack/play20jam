// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SafeZoneData.cs">
//   Copyright (c) 2020 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

[CreateAssetMenu(menuName = "Play2020/SafeZoneData", fileName = "SafeZoneData", order = 0)]
public class SafeZoneData: ScriptableObject
{
	[SerializeField, Range(0, 10)]
	private float size = 7;

	[SerializeField]
	private float startEnergy = 60;

	[SerializeField]
	private float requiredWater = 0.4f;

	public float Size => size;

	public float StartEnergy => startEnergy;

	public float RequiredWater => requiredWater;
}