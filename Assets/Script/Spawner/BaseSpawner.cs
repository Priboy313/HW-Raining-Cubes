using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class BaseSpawner<T> : MonoBehaviour, ISpawner, ISpawnerStats where T : PoolableObject
{
	[Header("Pool Settings")]
	[SerializeField] protected T _prefab;
	[SerializeField] protected int _poolCapacity = 40;
	[SerializeField] protected int _poolMaxSize = 40;

	protected ObjectPool<T> _pool;

	public int AllTimeSpawned { get; private set; } = 0;
	public int TotalCreated { get; private set; } = 0;
	public int ActiveCount { get { return _pool == null ? 0 : _pool.CountActive; } }

	public event Action<Vector3> ObjectReleased;
	public event Action OnStatsChanged;

	protected virtual void Awake()
	{
		_pool = new ObjectPool<T>(
			createFunc: CreatePooledObject,
			actionOnGet: OnTakeFromPool,
			actionOnRelease: OnReleaseToPool,
			actionOnDestroy: OnDestroyFromPool,
			collectionCheck: true,
			defaultCapacity: _poolCapacity,
			maxSize: _poolMaxSize
		);
	}

	protected void SpawnInternal(Vector3 position)
	{
		T instance = _pool.Get();
		instance.transform.position = position;
		InitPoolableObject(instance);
	}

	protected abstract void InitPoolableObject(T obj);

	private T CreatePooledObject()
	{
		TotalCreated++;
		OnStatsChanged?.Invoke();

		T instance = Instantiate(_prefab);
		instance.ReturnToPoolRequested += ReturnObjToPool;
		return instance;
	}

	private void OnTakeFromPool(T obj)
	{
		AllTimeSpawned++;
		OnStatsChanged?.Invoke();

		obj.gameObject.SetActive(true);
		obj.OnSpawn();
	}

	private void OnReleaseToPool(T obj)
	{
		obj.gameObject.SetActive(false);
		obj.OnDespawn();

		OnStatsChanged?.Invoke();
	}

	private void OnDestroyFromPool(T obj)
	{
		obj.ReturnToPoolRequested -= ReturnObjToPool;
		Destroy(obj.gameObject);
	}

	private void ReturnObjToPool(PoolableObject obj)
	{
		ObjectReleased?.Invoke(obj.transform.position);

		_pool.Release((T)obj);
	}
}
