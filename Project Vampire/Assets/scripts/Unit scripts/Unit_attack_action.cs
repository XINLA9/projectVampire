using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_attack_action : MonoBehaviour
{
    private Unit_attributes _attributes;
    private int attack;
    private float force;
    // Start is called before the first frame update
    void Start()
    {
        _attributes = GetComponent<Unit_attributes>();
        attack = _attributes.attack;
        force = _attributes.force;
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
            enemyState.HP -= attack;
            
            Vector3 away = other.transform.position - transform.position;
            enemyRb.AddForce(away * force, ForceMode.Impulse);
            //    playerAudio.PlayOneShot(crashSound, 1.0f);
            //    explosionParticle.Play();
        }
    }
}
