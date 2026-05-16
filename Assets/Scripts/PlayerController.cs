using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _ver;
    private float _hor;

    [SerializeField] private float _speed;

    // Update is called once per frame
    void Update()
    {
        _hor = Input.GetAxis("Horizontal");
        _ver = Input.GetAxis("Vertical");

        Vector3 move = transform.right * _hor + transform.forward * _ver;
        move = Vector3.ClampMagnitude(move, 1f);

        transform.position += move * _speed * Time.deltaTime;
    }
}
