using UnityEngine;
using System.Collections;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;
    [SerializeField]
    private GameObject _fireballPrefab;
    [SerializeField]
    private Transform _fireballsSpawnPoint;

    private float _shootDelay = 1f;

    private float _timeElapsedFromShoot = 1f;
    void Start()
    {
        _camera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        _timeElapsedFromShoot += Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            if (_timeElapsedFromShoot >= _shootDelay)
            {
                GameObject fireball = Instantiate(_fireballPrefab) as GameObject;
                fireball.transform.position = _fireballsSpawnPoint.position;
                fireball.transform.rotation = _fireballsSpawnPoint.rotation;
                _timeElapsedFromShoot = 0f;
            }
        }
    }

}