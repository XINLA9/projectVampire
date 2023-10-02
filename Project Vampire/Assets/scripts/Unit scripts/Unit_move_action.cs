using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMove : MonoBehaviour
{   
    private Unit_attributes _attributes;
    private float _moveSpeed;
    private float _rotationSpeed;
    private float _mapBound = 25.0f;
    private GameObject _moveGoal = null;
    private Rigidbody _rb;
    private bool _isDead;
    // Start is called before the first frame update
    void Start()
    {
        _attributes = GetComponent<Unit_attributes>();
        _moveSpeed = _attributes.maxSpeed;
        _rotationSpeed = _attributes.rotationSpeed;
        _rotationSpeed = _attributes.rotationSpeed;
        _isDead = _attributes.isDead;
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isDead)
        {
            FindNearestEnemy();
            MoveTowardsNearestHunter();
        }
        else
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }
        stayInMap();
    }
    private void FindNearestEnemy()
    {
        string goalTag;
        if (gameObject.tag == "hunter")
        {
            goalTag = "monster";
        }
        else { goalTag = "hunter"; }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(goalTag);

        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                _moveGoal = enemy;
            }
        }
    }
    private void MoveTowardsNearestHunter()
    {
        if (_moveGoal != null)
        {
            Vector3 lookDirection = (_moveGoal.transform.position - transform.position);
            lookDirection.y = 0f;
            lookDirection = lookDirection.normalized;

            // Calculate the target direction of rotation using Quaternion.LookRotation
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

            // Rotate the unit to face the target direction
            _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, Time.deltaTime * _rotationSpeed);

            Vector3 targetVelocity = lookDirection * _moveSpeed;
            _rb.velocity = targetVelocity;
        }
    }

    private void stayInMap()
    {
        if (transform.position.x > _mapBound)
        {
            transform.position = new Vector3(_mapBound - 0.5f, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -_mapBound)
        {
            transform.position = new Vector3(-_mapBound + 0.5f, transform.position.y, transform.position.z);
        }
        if (transform.position.z > _mapBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _mapBound - 0.5f);
        }
        if (transform.position.z < -_mapBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -_mapBound + 0.5f);
        }
        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        if (transform.position.y > 0.5f)
        {
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
