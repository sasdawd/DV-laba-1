using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float obstacleRange = 7.5f;
    private bool _alive;
    [SerializeField]
    private GameObject fireballPrefab;

    [SerializeField]
    private Transform _fireballSpawnPosition;

    private GameObject _fireball;

    private float _shootDelay = 1f;

    private float _timeElapsedFromShoot = 1f;
    private bool _isRotating = false;
    void Start()
    {
        _alive = true;
    }
    
    void Update()
    {
        _timeElapsedFromShoot += Time.deltaTime;
        if (_alive == true)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if(Physics.SphereCast(ray,0.75f, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.GetComponent<PlayerCharacter>())
            {
                if (_timeElapsedFromShoot >= _shootDelay)
                {
                    _fireball = Instantiate(fireballPrefab) as GameObject;
                    _fireball.transform.position = _fireballSpawnPosition.position;
                    _fireball.transform.rotation = transform.rotation;
                    _timeElapsedFromShoot = 0f;
                }
            }
            else if (hit.distance < obstacleRange&&!_isRotating)
            {
                float angle = Random.Range(0, 180);
                Debug.Log(angle);
                StartCoroutine(RotateEnemy(angle));
            }
        }    
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }

    private IEnumerator RotateEnemy(float angle)
    {
        _isRotating = true;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation;
        endRotation = Quaternion.Euler(endRotation.eulerAngles.x,
            endRotation.eulerAngles.y + angle,
            endRotation.eulerAngles.z);
        float progress = 0f, elapsedTime = 0f, duration = angle/180f;
        while (progress <= 1f)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, progress);
            elapsedTime += Time.unscaledDeltaTime;
            progress = elapsedTime / duration;
            yield return null;
        }
        _isRotating = false;
    }
}
