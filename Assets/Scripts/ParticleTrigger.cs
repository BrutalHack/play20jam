// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParticleTrigger.cs">
//   Copyright (c) 2020 Johannes Deml. All rights reserved.
// </copyright>
// <author>
//   Johannes Deml
//   public@deml.io
// </author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
	[SerializeField]
	private ParticleSystem particleSys = null;

	[SerializeField]
	private float particleLifetimeChange = -2f;

    [SerializeField]
    private Color32 particleAlphaChange = new Color32(255,255,255,255);

    [SerializeField]
	private float range = 5f;

	private ParticleSystem.Particle[] particles;

	void Awake()
	{
		particles = new ParticleSystem.Particle[particleSys.main.maxParticles];
	}

	void LateUpdate()
	{
		var targetPosition = transform.position;
		var squaredRange = range * range;

		int numAliveParticles = particleSys.GetParticles(particles);
		int particleCounter = 0;

		for (int i = 0; i < numAliveParticles; i++)
		{
			var particle = particles[i];
			var particlePosition = particle.position;
			var particleToTarget = targetPosition - particlePosition;
			var sqrDistance = Vector3.SqrMagnitude(particleToTarget);

			if (sqrDistance > squaredRange + 0.01f)
			{
				particles[particleCounter++] = particle;
				continue;
			}

			particle.remainingLifetime += particleLifetimeChange * Time.deltaTime;
            particle.color = particleAlphaChange;
			particles[particleCounter++] = particle;
		}

		particleSys.SetParticles(particles, particleCounter);
	}

	#if UNITY_EDITOR

	void Reset()
	{
		if (particleSys == null)
		{
			particleSys = GetComponent<ParticleSystem>();
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, range);
	}
	#endif
}