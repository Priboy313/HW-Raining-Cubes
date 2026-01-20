using System;
using UnityEngine;

public interface ISpawnerStats
{
	public int AllTimeSpawned { get; }
	public int TotalCreated { get; }
	public int ActiveCount { get; }

	public event Action OnStatsChanged;
}
