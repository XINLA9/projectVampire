using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMove : MonoBehaviour
{   
    private Unit_attributes _attributes;
    private float _moveSpeed;
    private float _rotationSpeed;
    private float mapBound = 25.0f;
    private GameObject moveGoal;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        _attributes = GetComponent<Unit_attributes>();
        _moveSpeed = _attributes.maxSpeed;
        _rotationSpeed = _attributes.rotationSpeed;
        _rotationSpeed = _attributes.rotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        rb = GetComponent<Rigidbody>();
        FindNearestEnemy();
        MoveTowardsNearestHunter();
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
                moveGoal = enemy;
            }
        }
    }
    private void MoveTowardsNearestHunter()
    {
        if (moveGoal != null)
        {
            Vector3 lookDirection = (moveGoal.transform.position - transform.position);
            lookDirection.y = 0f;
            lookDirection = lookDirection.normalized;

            // Calculate the target direction of rotation using Quaternion.LookRotation
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

            // Rotate the unit to face the target direction
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);

            Vector3 targetVelocity = lookDirection * _moveSpeed;

            //Vector3 velocityDiff = targetVelocity - rb.velocity;

            //Vector3 accelerationVec = velocityDiff / Time.fixedDeltaTime;

            //accelerationVec = Vector3.ClampMagnitude(accelerationVec, this._acceleration);
            //accelerationVec.y = 0f;

            //rb.AddForce(accelerationVec, ForceMode.Impulse);
            // Apply the target velocity directly to the position using transform
            transform.position += targetVelocity * Time.deltaTime;
        }
    }

    private void stayInMap()
    {
        if (transform.position.x > mapBound)
        {
            transform.position = new Vector3(mapBound - 0.5f, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -mapBound)
        {
            transform.position = new Vector3(-mapBound + 0.5f, transform.position.y, transform.position.z);
        }
        if (transform.position.z > mapBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, mapBound - 0.5f);
        }
        if (transform.position.z < -mapBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -mapBound + 0.5f);
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
