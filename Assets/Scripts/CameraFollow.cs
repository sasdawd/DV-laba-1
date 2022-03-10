using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _player;
    private float offsetX;
    private float offsetY;
    private float offsetZ;

    // Start is called before the first frame update
    void Start()
    {
        offsetX = _player.position.x - transform.position.x;
        offsetY = _player.position.y - transform.position.y;
        offsetZ = _player.position.z - transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (_player != null)
        {
            transform.position = new Vector3(_player.transform.position.x - offsetX,
                _player.transform.position.y - offsetY,
                _player.transform.position.z - offsetZ);
            transform.forward = _player.forward;
        }
    }
}
