using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Move : MonoBehaviour
{
    private Attributes _attributes;
    private Animator _animator;
    private Rigidbody _rb;
    public GameObject _moveGoal = null;
    private float _maxSpeed;
    private float _acceleration;
    private float _rotationSpeed;
    private float _mapBound = 50.0f;
    private bool _isDead;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        _attributes = GetComponent<Attributes>();
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        // Get necessary attributes from the object attribute script
        _maxSpeed = _attributes.maxSpeed;
        _rotationSpeed = _attributes.rotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        _isDead = _attributes.isDead;
        _moveGoal = _attributes.moveGoal;
        _acceleration = _attributes.acceleration;
        speed = _rb.velocity.magnitude;

        if (!_isDead && _moveGoal != null)
        {
            MoveTowardsNearestHunter();
            _animator.SetFloat("speed", speed);
        }
        else
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            _animator.SetFloat("speed", speed);
        }
        if (_attributes.moveGoal == null)
        {
            _animator.SetBool("noEnemy", true);
        }
        else
        {
            _animator.SetBool("noEnemy", false);
        }
        stayInMap();
    }
    private void MoveTowardsNearestHunter()
    {
        Vector3 lookDirection = (_moveGoal.transform.position - transform.position);
        lookDirection.y = 0f;
        lookDirection = lookDirection.normalized;

        // Calculate the _target direction of rotation using Quaternion.LookRotation
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

        float angle = Quaternion.Angle(transform.rotation, targetRotation);

        _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
        // Define the minimum angle at which acceleration can start
        float minAngleToAccelerate = 10f;

        if (angle <= minAngleToAccelerate)
        {
            Vector3 forward = gameObject.transform.forward;
            forward.y = 0;
            forward = forward.normalized;
            _rb.AddForce(forward * _acceleration * Time.deltaTime, ForceMode.VelocityChange);
        }
        else if (angle > minAngleToAccelerate)
        {
            _rb.velocity = Vector3.zero;
        }

        // Limit the maximum velocity to _maxSpeed
        if (speed > _maxSpeed)
        {
            _rb.velocity = _rb.velocity.normalized * _maxSpeed;
        }
    }
    private void OnCollisionStay(Collision other)
    {
        string alleyTag;
        string enemyTag;
        if (gameObject.tag == "hunter")
        {
            enemyTag = "monster";
        }
        else
        {
            enemyTag = "hunter";
        }
        if (other.gameObject.CompareTag(enemyTag))
        {
            _rb.velocity = Vector3.zero;
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
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
