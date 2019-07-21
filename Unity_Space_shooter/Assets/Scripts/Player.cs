using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Private variable naming pattern in c#
    public int imNotPrivate = 1;
    private int _imPrivate = 1;

    // Speed variable
    [SerializeField]    // Permit manual adjustment using the Unity interface even when variable is private!
    private float _speed = 5.0f;
    
    // Boundaries variables
    private float _hLimit = 11.3f;
    private float _vLimit = 6.5f;

    // Start is called before the first frame update
    void Start()
    {
        // Initial position
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement ()
    {
        // Check input info in Unity: edit >> project settings... >> Input
        float input_horizontal = Input.GetAxis("Horizontal");
        float input_vertical = Input.GetAxis("Vertical");

        // Translation
        //transform.Translate(Vector3.right * input_horizontal * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * input_vertical * _speed * Time.deltaTime);
        transform.Translate(new Vector3(input_horizontal, input_vertical, 0) * _speed * Time.deltaTime);

        // Simple player bounds
        //transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -_vLimit, _vLimit), transform.position.z);
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, -_hLimit, _hLimit), transform.position.y, transform.position.z);
        
        // Loop player bounds
        if (transform.position.x < -_hLimit)
        {
            transform.position = new Vector3(_hLimit, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > _hLimit)
        {
            transform.position = new Vector3(-_hLimit, transform.position.y, transform.position.z);
        }

        if (transform.position.y < -_vLimit)
        {
            transform.position = new Vector3(transform.position.x, _vLimit, transform.position.z);
        }
        else if (transform.position.y > _vLimit)
        {
            transform.position = new Vector3(transform.position.x, -_vLimit, transform.position.z);
        }
    }
}
