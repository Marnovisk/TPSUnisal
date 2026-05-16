using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class TPSController : MonoBehaviour
{
    private float _mouseHor;
    private float _mouseVer;

    public GameObject _head;
    public GameObject _headLook;

    public Transform _gunPoint;

    [SerializeField] private float _sensibility;

    [SerializeField] private Transform _aimTarget;

    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletLifeTime;
    [SerializeField] private float _bulletDamage;

    private Vector3 mouseWorldPosition = Vector3.zero;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private Cinemachine3rdPersonFollow _thirdPersonFollow;

    private string _enemyTag;


    // Start is called before the first frame update
    void Start()
    {
        _thirdPersonFollow = _camera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        _thirdPersonFollow.ShoulderOffset = new Vector3(0.5f, -1f, -3.5f);
        mouseWorldPosition = Vector3.zero;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _enemyTag = "Enemy";
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        AimTarget();
    }

    void Aim()
    {
        float mouseX = Input.GetAxis("Mouse X") * _sensibility;
        float mouseY = Input.GetAxis("Mouse Y") * _sensibility;

        _mouseHor += mouseX;
        _mouseVer -= mouseY;

        _mouseVer = Mathf.Clamp(_mouseVer, -50f, 80f);

        transform.rotation = Quaternion.Euler(0f, _mouseHor, 0f);
        _head.transform.localRotation = Quaternion.Euler(_mouseVer, 0f, 0f);
    }

    void AimTarget()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 999))
        {
            mouseWorldPosition = hitInfo.point;
        }
        else
        {
            mouseWorldPosition = ray.GetPoint(100);
        }

        _aimTarget.position = mouseWorldPosition;

        if (Input.GetButton("Fire2"))
        {            
             _headLook.transform.LookAt(_aimTarget);  
             _thirdPersonFollow.ShoulderOffset = new Vector3(1f, -1f, -1f); 
             if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Fire!");
                Vector3 direction = (mouseWorldPosition - _gunPoint.position).normalized;
                Quaternion rotation = Quaternion.LookRotation(direction);

                var bullet = Instantiate(_bullet, _gunPoint.position, rotation);
                bullet.GetComponent<BulletScript>().Init(_bulletSpeed, _bulletLifeTime, _bulletDamage, _enemyTag);
            }
            
        }
        else
        {
            _thirdPersonFollow.ShoulderOffset = new Vector3(1f, -1f, -3.5f);
            _headLook.transform.LookAt(_aimTarget);
        }
    }
}