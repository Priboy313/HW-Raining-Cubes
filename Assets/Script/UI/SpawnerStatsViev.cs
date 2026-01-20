using TMPro;
using UnityEngine;

public class SpawnerStatsViev : MonoBehaviour
{
	[SerializeField] private GameObject _targetSpawner;
	[SerializeField] private TextMeshProUGUI _statsText;

	private ISpawnerStats _spawnerStats;

	private void Awake()
	{
		if (_targetSpawner != null)
		{
			_spawnerStats = _targetSpawner.GetComponent<ISpawnerStats>();
		}

		if (_spawnerStats == null)
		{
			Debug.LogError("SpawnerStats not set!");
			enabled = false;
		}
	}

	private void OnEnable()
	{
		if (_spawnerStats != null)
		{
			_spawnerStats.OnStatsChanged += UpdateUI;
			UpdateUI();
		}
	}

	private void OnDisable()
	{
		if (_spawnerStats != null)
		{
			_spawnerStats.OnStatsChanged -= UpdateUI;
		}
	}

	private void UpdateUI()
	{
		float spawned = _spawnerStats.AllTimeSpawned;
		float created = _spawnerStats.TotalCreated;
		float active = _spawnerStats.ActiveCount;

		_statsText.text = 
			$"Spawned: {spawned}\r\n" +
			$"Created: {created}\r\n" +
			$"Active: {active}";
	}
}
