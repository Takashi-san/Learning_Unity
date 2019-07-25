using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
	[SerializeField]
	private float _enemySpawnRate = 5.0f;
	[SerializeField]
	private GameObject _enemyContainer;
	[SerializeField]
	private GameObject _enemyPrefab;
	[SerializeField]
	private GameObject[] _powerupPrefab;
	private bool _stopSpawning = false;

	void Start() {
		StartCoroutine(SpawnEnemy());
		StartCoroutine(SpawnPowerup());
	}

	void Update() {

	}

	IEnumerator SpawnEnemy() {
		while (!_stopSpawning) {
			GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-Constants.hLimit, Constants.hLimit), Constants.vLimit, 0), Quaternion.identity);
			newEnemy.transform.parent = _enemyContainer.transform;
			yield return new WaitForSeconds(_enemySpawnRate);
		}
	}

	IEnumerator SpawnPowerup() {
		while (!_stopSpawning) {
			yield return new WaitForSeconds(Random.Range(3, 8));
			Instantiate(_powerupPrefab[Random.Range(0, 3)], new Vector3(Random.Range(-Constants.hLimit, Constants.hLimit), Constants.vLimit, 0), Quaternion.identity);
		}
	}

	public void PlayerDied() {
		_stopSpawning = true;
	}
}
