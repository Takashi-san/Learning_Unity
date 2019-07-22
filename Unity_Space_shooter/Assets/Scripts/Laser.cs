using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    [SerializeField]
    private float _range = 8.0f;
    private float _traveled = 0;

    // Update is called once per frame
    void Update()
    {
        Movement();
        
        // Selfdestruct
        _traveled += _speed * Time.deltaTime;
        if (_traveled > _range)
        {
            Destroy(gameObject);
        }
    }

    void Movement()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }
}
