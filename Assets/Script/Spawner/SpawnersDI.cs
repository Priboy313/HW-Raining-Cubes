using UnityEngine;

public class SpawnersDI : MonoBehaviour
{
	[SerializeField] private RainableSpawner _spawnerRainable;
	[SerializeField] private BombSpawner _spawnerBomb;

	private void Awake()
	{
		if (_spawnerRainable != null && _spawnerBomb != null)
		{
			_spawnerBomb.Init(_spawnerRainable);
		}
	}
}
