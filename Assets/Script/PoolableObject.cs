using System;
using UnityEngine;

public abstract class PoolableObject : MonoBehaviour
{
	public event Action<PoolableObject> ReturnToPoolRequested;

	public virtual void OnSpawn()
	{

	}

	public virtual void OnDespawn()
	{

	}

	protected void ReturnToPool()
	{
		ReturnToPoolRequested?.Invoke(this);
	}
}
