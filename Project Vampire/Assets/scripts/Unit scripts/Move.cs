using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Move : MonoBehaviour
{
    private Attributes _attributes;
    private Animator _animator;
    private Rigidbody _rb;
    public GameObject _moveGoal = null;
    private NavMeshAgent _agent;
    private float _acceleration;
    private float _mapBound = 50.0f;
    private bool _isDead;

    public float current_speed;
    // Start is called before the first frame update
    void Start()
    {
        _attributes = GetComponent<Attributes>();
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        // Get necessary attributes from the object attribute script
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _attributes.maxSpeed;
        _agent.acceleration = _attributes.acceleration;
    }

    // Update is called once per frame
    void Update()
    {
        _isDead = _attributes.isDead;
        _moveGoal = _attributes.moveGoal;
        current_speed = _agent.velocity.magnitude;

        _animator.SetFloat("speed", current_speed);

        if (!_isDead && _moveGoal != null)
        {
            _agent.speed = _attributes.maxSpeed;
            _agent.destination = _moveGoal.transform.position;
        }
 
        if(_isDead)
        {
            _agent.speed = 0;
            _rb.velocity = Vector3.zero;
        }

        if (_attributes.moveGoal == null)
        {
            _animator.SetBool("noEnemy", true);
            _agent.speed = 0;
            _rb.velocity = Vector3.zero;
        }
        else
        {
            _animator.SetBool("noEnemy", false);
        }

    }

    private void OnCollisionStay(Collision other)
    {
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
            _agent.speed = _attributes.maxSpeed / 10;
        }
    }
}
