using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeScript : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPefab;
    [SerializeField] private GameObject _player;
    [SerializeField] private int _currentHorde;
    [SerializeField] private int _enemyCount = 0;
    [SerializeField] private int _enemyInScreen;
    [SerializeField] private int _maxEnemies;
    [SerializeField] private List<Transform> _spawnLocation = new List<Transform>();

    [SerializeField] private float _sightRadius;
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _baseSpeed;
    [SerializeField] private float _bulletLifeTime;
    [SerializeField] private float _bulletCooldown;
    [SerializeField] private float _bulletDamage;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _SpawnTimer = 5;
    [SerializeField] private float _currentTime;

    private void Start()
    {
        StartHorde();
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        VerifyCanSapwn();
    }

    void StartHorde()
    {
        if(_enemyCount <= _maxEnemies)
        {
            Transform selectedLocation = _spawnLocation[Random.Range(0, _spawnLocation.Count)];
            SpawnEnemy(selectedLocation);
        }
    }

    void SpawnEnemy(Transform selectedLocation)
    {
        var enemy = Instantiate(_enemyPefab, selectedLocation);
        enemy.GetComponent<EnemyScript>().Init(_player, _sightRadius, _attackRadius,
            _baseSpeed, _bulletCooldown, _bulletLifeTime, _bulletDamage, _bulletSpeed);
        _enemyCount += 1;
    }

    void VerifyCanSapwn()
    {
        if(_currentTime >= _SpawnTimer)
        {
            StartHorde();
            _currentTime = 0;
        }
    }
}
