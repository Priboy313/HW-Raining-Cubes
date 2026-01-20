using System.Collections;
using UnityEngine;

public class RainableSpawner : BaseSpawner<Rainable>
{
	[Header("Spawn Specifics")]
	[SerializeField] private float _spawnInterval = 1f;
	[SerializeField] private Transform _startPoint;
	[SerializeField] private Transform _endPoint;

	[Header("Rainable Config")]
	[SerializeField] private float _lifetimeMin = 2f;
	[SerializeField] private float _lifetimeMax = 5f;
	[SerializeField] private Color _defaultColor;

	private void Start()
	{
		StartCoroutine(SpawnRoutine());
	}

	protected override void InitPoolableObject(Rainable obj)
	{
		obj.Init(_defaultColor, _lifetimeMin, _lifetimeMax);
		obj.Rigidbody.velocity = Vector3.zero;
		obj.Rigidbody.angularVelocity = Vector3.zero;
	}

	private IEnumerator SpawnRoutine()
	{
		while (enabled)
		{
			Vector3 position = DevUtils.GetRandomVector3(_startPoint.position, _endPoint.position);
			SpawnInternal(position);
			yield return new WaitForSeconds(_spawnInterval);
		}
	}

}
