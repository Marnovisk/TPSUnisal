using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _sightRadius;
    [SerializeField] private float _attackRadius;
    private float _distanceToPlayer;
    private float _currentSpeed;
    [SerializeField] private float _baseSpeed;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletLifeTime;
    [SerializeField] private float _bulletCooldown;
    [SerializeField] private float _bulletDamage;
    private float _currentCooldown;
    [SerializeField] private string _playerTag;
    [SerializeField] private EnemyState _state;


    public void Init(GameObject player, float sightRadius,
        float attackRadius, float baseSpeed, float bulletCooldown, 
        float bulletLifeTime, float bulletDamage, float bulletSpeed)
    {
        _sightRadius = sightRadius;
        _attackRadius = attackRadius;
        _baseSpeed = baseSpeed;
        _bulletCooldown = bulletCooldown;
        _bulletLifeTime = bulletLifeTime;
        _bulletDamage = bulletDamage;
        _playerTag = "Player";
        _bulletDamage = bulletSpeed;
        _player = player;
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();

        switch (_state)
        {
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Chase:
                Chase();
                break;
            case EnemyState.Idle:
                Idle();
                break;
        }
    }

    void DetectPlayer()
    {
        _distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
        if (_distanceToPlayer <= _sightRadius)
        {
            if (_distanceToPlayer <= _attackRadius)
            {
                _state = EnemyState.Attack;
            }
            else
            {
                _state = EnemyState.Chase;
            }
        }
        else
        {
            _state = EnemyState.Idle;
        }
    }

    void Attack()
    {
        LookAtPlayer();
        _currentCooldown += 1f * Time.deltaTime;
        if (_currentCooldown >= _bulletCooldown)
        {
            var bullet = Instantiate(_bullet, _firePoint.position, _firePoint.rotation);
            bullet.GetComponent<BulletScript>().Init(_bulletSpeed, _bulletLifeTime, _bulletDamage, _playerTag);
            _currentCooldown = 0f;
        }
        
    }
    void Idle() 
    {
        _currentSpeed = _baseSpeed / 2;
        LookAtPlayer();
        ChasePlayer();
    }

    void Chase()
    {
        _currentSpeed = _baseSpeed;
        LookAtPlayer();
        ChasePlayer();
    }

    void ChasePlayer()
    {
        Vector3 move = _player.transform.position - transform.position;
        move = Vector3.ClampMagnitude(move, 1f);
        transform.position += move * _currentSpeed * Time.deltaTime;
    }

    void LookAtPlayer()
    {
        transform.LookAt(_player.transform.position);
    }
}
