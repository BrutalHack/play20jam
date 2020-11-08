// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Audio.cs">
//   Copyright (c) 2020 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

public class Audio: MonoBehaviour
{
	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
}