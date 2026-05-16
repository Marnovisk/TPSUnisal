using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _minHealth;
    [SerializeField] private float _currentHealth;
    private string _actorTag;
    void Awake()
    {
        _currentHealth = _maxHealth;
        _actorTag = this.gameObject.tag;
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= _minHealth)
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
