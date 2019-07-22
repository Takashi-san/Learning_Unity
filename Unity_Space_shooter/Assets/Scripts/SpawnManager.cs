using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private float _enemySpawnRate = 5.0f;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _tripleShotPUPrefab;
    private bool _stopSpawning = false;

    private float _vLimit = 6.5f;
    private float _hLimit = 11.3f;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnTripleShotPU());
    }

    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        while (!_stopSpawning)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-_hLimit, _hLimit), _vLimit, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_enemySpawnRate);
        }
    }

    IEnumerator SpawnTripleShotPU()
    {
        while (!_stopSpawning)
        {
            yield return new WaitForSeconds(Random.Range(3, 8));
            Instantiate(_tripleShotPUPrefab, new Vector3(Random.Range(-_hLimit, _hLimit), _vLimit, 0), Quaternion.identity);
        }
    }

    public void PlayerDied()
    {
        _stopSpawning = true;
    }
}
