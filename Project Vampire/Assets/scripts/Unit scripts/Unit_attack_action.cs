using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_attack_action : MonoBehaviour
{
    private Unit_attributes _attributes;
    private int _attack;
    private float _force;
    private bool _isDead;
    // Start is called before the first frame update
    void Start()
    {
        _attributes = GetComponent<Unit_attributes>();
        _attack = _attributes.attack;
        _force = _attributes.force;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (!other.gameObject.CompareTag(gameObject.tag))
        {
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
            Unit_attributes enemyState = other.gameObject.GetComponent<Unit_attributes>();
            enemyState.HP -= _attack;
            
            Vector3 away = other.transform.position - transform.position;
            enemyRb.AddForce(away * _force, ForceMode.Impulse);
            //    playerAudio.PlayOneShot(crashSound, 1.0f);
            //    explosionParticle.Play();
        }
    }
}
