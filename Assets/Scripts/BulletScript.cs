using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private string _tag;
    private bool _enabled = false;

    public void Init(float speed, float lifeTime, float damage, string tag)
    {
        Destroy(gameObject, lifeTime);
        _speed = speed;
        _damage = damage;
        _tag = tag;
        _enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (!_enabled) return; 
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == _tag)
        {
            other.gameObject.GetComponent<HealthScript>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
