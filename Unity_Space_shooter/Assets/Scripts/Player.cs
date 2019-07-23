﻿using System.Collections;
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
    [SerializeField]
    private float _speedUpBonus = 3.0f;
    [SerializeField]
    private float _speedUpUpTime = 5.0f;
    private float _totalSpeedBonus = 0;
    
    // Boundaries variables
    private float _hLimit = 11.3f;
    private float _vLimit = 6.5f;

    // Shooting variables
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private Vector3 _laserOffset = new Vector3(0f, 0.8f, 0f);
    [SerializeField]
    private float _laserFireRate = 0.5f;
    private float _laserCanFire = 0.0f;
    [SerializeField]
    private bool _tripleShotFlag = false;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _tripleShotUpTime = 5.0f;

    [SerializeField]
    private int _hp = 3;
    private SpawnManager _spawnManager;
    
    [SerializeField]
    private bool _shieldActive = false;
    private Transform _shieldTransform;

    // Start is called before the first frame update
    void Start()
    {
        // Initial position
        transform.position = new Vector3(0, 0, 0);

        //_spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _spawnManager = GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManager>();

        if (!_spawnManager)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }

        _shieldTransform = transform.Find("Player_Shield");
        if (!_shieldTransform)
        {
            Debug.LogError("Player_Shield is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
        if (Input.GetKey(KeyCode.Space)) 
        {
            if (Time.time > _laserCanFire)
            {
                Shoot();
            }
        }
    }

    void Movement()
    {
        // Check input info in Unity: edit >> project settings... >> Input
        float input_horizontal = Input.GetAxis("Horizontal");
        float input_vertical = Input.GetAxis("Vertical");

        // Translation
        //transform.Translate(Vector3.right * input_horizontal * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * input_vertical * _speed * Time.deltaTime);
        transform.Translate(new Vector3(input_horizontal, input_vertical, 0) * (_speed + _totalSpeedBonus) * Time.deltaTime);

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

    void Shoot()
    {
        GameObject newShot;
        if (_tripleShotFlag)
        {
            newShot = Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else 
        {
            newShot = Instantiate(_laserPrefab, transform.position + _laserOffset, Quaternion.identity);
        }
        _laserCanFire = Time.time + _laserFireRate;
    }

    public void DealDamage(int dmg = 1)
    {
        if (_shieldActive)
        {
            _shieldActive = false;
            _shieldTransform.gameObject.SetActive(false);
            return;
        }
        
        _hp -= dmg;
        if (_hp <= 0)
        {
            _spawnManager.PlayerDied();
            Destroy(gameObject);
        }
    }

    public void TripleShotEnable()
    {
        _tripleShotFlag = true;
        StopCoroutine("TripleShotPowerDown");
        StartCoroutine("TripleShotPowerDown");
    }

    public void SpeedUpEnable()
    {
        _totalSpeedBonus += _speedUpBonus;
        StopCoroutine("SpeedReset");
        StartCoroutine("SpeedReset");
    }

    public void ShieldActivate()
    {
        _shieldActive = true;
        _shieldTransform.gameObject.SetActive(true);
    }

    IEnumerator TripleShotPowerDown()
    {
        yield return new WaitForSeconds(_tripleShotUpTime);
        _tripleShotFlag = false;
    }

    IEnumerator SpeedReset()
    {
        yield return new WaitForSeconds(_speedUpUpTime);
        _totalSpeedBonus = 0;
    }
}
