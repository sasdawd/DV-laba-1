using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float obstacleRange = 5f;
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
        Ray ray = new Ray(new Vector3(transform.position.x,transform.position.y+1f,transform.position.z), transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            Debug.DrawRay(ray.origin, ray.direction);
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
                float angle = 0f;
                Vector3 point;
                if (transform.rotation.eulerAngles.y <= 90f)
                {
                    point = hit.point;
                    point.z -= hit.collider.transform.localScale.z;
                    if (point.z > transform.position.z)
                        angle = Random.Range(90f, 180f);
                    else angle = Random.Range(-180f, -90f);
                }
                else if(transform.rotation.eulerAngles.y > 90f&&transform.rotation.eulerAngles.y<=180f)
                {
                    point = hit.point;
                    point.x -= hit.collider.transform.localScale.x;
                    if (point.x > transform.position.x)
                        angle = Random.Range(90f, 180f);
                    else angle = Random.Range(-180f, -90f);
                }
                else if (transform.rotation.eulerAngles.y > 180f && transform.rotation.eulerAngles.y <= 270f)
                {
                    point = hit.point;
                    point.z += hit.collider.transform.localScale.z;
                    if (point.z < transform.position.z)
                        angle = Random.Range(90f, 180f);
                    else angle = Random.Range(-180f, -90f);
                }
                else
                {
                    point = hit.point;
                    point.x += hit.collider.transform.localScale.x;
                    if (point.x < transform.position.x)
                        angle = Random.Range(90f, 180f);
                    else angle = Random.Range(-180f, -90f);
                }
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
        float progress = 0f, elapsedTime = 0f, duration = Mathf.Abs(angle)/360f;
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
