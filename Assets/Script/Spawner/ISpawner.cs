using System;
using UnityEngine;

public interface ISpawner
{
	public event Action<Vector3> ObjectReleased;
}
