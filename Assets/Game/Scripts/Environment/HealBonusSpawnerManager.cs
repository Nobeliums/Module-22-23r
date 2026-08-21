using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class HealBonusSpawnerManager : MonoBehaviour
{
	[SerializeField] private HealBonus _healBonusPrefab;
	[SerializeField] private float _timeToSpawn;

	[SerializeField] private Transform _spawnCenter;
	[SerializeField] private float _spawnRadius;
	
	private ObjectSpawner _objectSpawner;
	private Coroutine _spawnCoroutine;

	private void Awake()
	{
		if (_healBonusPrefab == null)
			Debug.LogWarning("HealBonusSpawnController: HealBonusPrefab is null");
		
		_objectSpawner = new ObjectSpawner(_healBonusPrefab.gameObject);
	}

	private void Update()
	{
		if (InputManager.GetSpawnerSwitchKey())
		{
			if (_spawnCoroutine != null)
			{
				StopCoroutine(_spawnCoroutine);
				_spawnCoroutine = null;
				Debug.Log($"Спавнер выключен");
			}
			else
			{
				_spawnCoroutine = StartCoroutine(StartSpawner());
				Debug.Log("Спавнер включен");
			}
		}
	}

	private IEnumerator StartSpawner()
	{
		while (true)
		{
			yield return new WaitForSeconds(_timeToSpawn);

			Vector3  spawnPosition = GetRandomSpawnPoint();

			_objectSpawner.Spawn(spawnPosition);
		}
	}

	private Vector3 GetRandomSpawnPoint()
	{
		float xSpawnPosition = Random.Range(-1f, 1f);
		float zSpawnPosition = Random.Range(-1f, 1f);
		
		Vector3 spawnPosition = new Vector3(xSpawnPosition, 0.0f, zSpawnPosition).normalized * _spawnRadius;
		
		spawnPosition += _spawnCenter.position;
		
		return spawnPosition;
	}
}