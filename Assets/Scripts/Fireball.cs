using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10.0f;
    public int damage = 1;

    private float _elapsedTime = 0f;

    void Start()
    {
        
    }

  
    void Update()
    {
        _elapsedTime += Time.deltaTime;
        transform.Translate(0, 0, speed * Time.deltaTime);
        if (_elapsedTime>4f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        PlayerCharacter player = col.GetComponent<PlayerCharacter>();
        if(player != null)
        {
            player.Hit(damage);
        }
        Destroy(this.gameObject);
    }
}


