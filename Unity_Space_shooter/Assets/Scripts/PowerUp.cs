using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField] // 0 = tripleshot; 1 = speed; 2 = shield.
    private int _powerUpID = 0;
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
                switch(_powerUpID)
                {
                    case 0:
                        player.TripleShotEnable();
                    break;

                    case 1:
                        player.SpeedUpEnable();
                    break;

                    case 2:
                        Debug.Log("shield powerup");
                    break;

                    default:
                        Debug.LogError("Invalid powerup ID!");
                    break;
                }
            }
            Destroy(gameObject);
        }
    }
}
