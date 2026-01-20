using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Colorable), typeof(Exploder))]
public class Bomb : PoolableObject
{
	private float _explosionForce = 1f;
	private float _explosionRadius = 1f;
	private float _lifetime = 2;

	private Rigidbody _rigidbody;
	private Colorable _colorable;
	private Exploder _exploder;

	public Rigidbody Rigidbody => _rigidbody;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_colorable = GetComponent<Colorable>();
		_exploder = GetComponent<Exploder>();
	}

	public void Init(
		Color color, 
		float lifetimeMin, 
		float lifetimeMax,
		float explosionForce,
		float explosionRaidus
		)
	{
		_colorable.SetColor(color);
		_lifetime = DevUtils.GetRandomNumber(lifetimeMin, lifetimeMax);
		_explosionForce = explosionForce;
		_explosionRadius = explosionRaidus;

		StartCoroutine(StartTimerToDestroy());
	}

	public override void OnDespawn()
	{
		_exploder.ApplyExplosionForceAround(_explosionForce, _explosionRadius);
	}

	private IEnumerator StartTimerToDestroy()
	{
		float timer = _lifetime;

		while (timer > 0)
		{
			timer -= Time.deltaTime;
			_colorable.SetAlpha(timer, _lifetime);

			yield return null;
		}

		ReturnToPool();
	}
}
