// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnergyUi.cs">
//   Copyright (c) 2020 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using Supyrb;
using UnityEngine;

public class EnergyUi: MonoBehaviour
{
	[SerializeField]
	private RectTransform progressbar = null;

	private PlayerEnergyLevelChangedSignal playerEnergyLevelChangedSignal;
	private Vector2 anchorMax;

	private void Awake()
	{
		anchorMax = progressbar.anchorMax;
        
		Signals.Get(out playerEnergyLevelChangedSignal);

		playerEnergyLevelChangedSignal.AddListener(OnLevelChanged);
	}

	private void OnDestroy()
	{
		playerEnergyLevelChangedSignal.RemoveListener(OnLevelChanged);
	}

	private void OnLevelChanged(float level)
	{
		anchorMax.x = level;
		progressbar.anchorMax = anchorMax;
	}
}