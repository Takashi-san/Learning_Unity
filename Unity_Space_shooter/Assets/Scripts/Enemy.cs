using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private float _vLimit = 6.5f;
    private float _hLimit = 11.3f;
    [SerializeField]
    private float _randomRange = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -_vLimit)
        {
            transform.position = new Vector3(transform.position.x + Random.Range(-_randomRange, _randomRange), _vLimit, 0);
            if ((transform.position.x < -_hLimit) || (transform.position.x > _hLimit))
            {
                transform.position = new Vector3(Random.Range(-_randomRange, _randomRange), _vLimit, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player)
            {
                player.DealDamage();
            }
            Destroy(gameObject);
        }
        else if (other.tag.Equals("Laser"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
