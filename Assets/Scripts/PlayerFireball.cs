using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireball : MonoBehaviour
{
    public float speed = 10.0f;

    private float _elapsedTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;
        transform.Translate(0, 0, speed * Time.deltaTime);
        if (_elapsedTime > 4f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        ReactiveTarget enemy = col.GetComponent<ReactiveTarget>();
        if (enemy != null)
        {
            enemy.ReactToHit();
        }
        Destroy(this.gameObject);
    }
}
