using UnityEngine;

public class BombSpawner : BaseSpawner<Bomb> 
{
	[Header("Bomb Config")]
	[SerializeField] private float _lifetimeMin = 2f;
	[SerializeField] private float _lifetimeMax = 5f;
	[SerializeField] private Color _defaultColor;
	[SerializeField] private float _explosionForce = 1f;
	[SerializeField] private float _explosionRadius = 1f;

	private ISpawner _targetSpawner;

	public void Init(ISpawner targetSpawner)
	{
		_targetSpawner = targetSpawner;
		_targetSpawner.ObjectReleased += OnTargetDespawned;
	}

	protected override void InitPoolableObject(Bomb obj)
	{
		obj.Init(_defaultColor, _lifetimeMin, _lifetimeMax, _explosionForce, _explosionRadius);
		obj.Rigidbody.velocity = Vector3.zero;
		obj.Rigidbody.angularVelocity = Vector3.zero;
	}

	private void OnTargetDespawned(Vector3 position)
	{
		SpawnInternal(position);
	}

	private void OnDisable()
	{
		if (_targetSpawner != null)
		{
			_targetSpawner.ObjectReleased -= OnTargetDespawned;
		}
	}
}
