using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotPU : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    private float _vLimit = 6.5f;

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -_vLimit)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player)
            {
                player.TripleShotEnable();
            }
            Destroy(gameObject);
        }
    }
}
