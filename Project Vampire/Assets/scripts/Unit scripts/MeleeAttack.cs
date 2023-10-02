using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_attack_action : MonoBehaviour
{
    private Attributes _attributes;
    private int _attack;
    private float _force;
    private bool _isDead;
    // Start is called before the first frame update
    void Start()
    {
        _attributes = GetComponent<Attributes>();
        
    }

    // Update is called once per frame
    void Update()
    {
        _attack = _attributes.attack;
        _force = _attributes.force;
        _isDead = _attributes.isDead;
    }
    private void OnCollisionEnter(Collision other)
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
        // If enemy collides with either goal, destroy it
        if (other.gameObject.CompareTag(enemyTag) && !_isDead)
        {
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
            Attributes enemyState = other.gameObject.GetComponent<Attributes>();
            enemyState.HP -= _attack;
            
            Vector3 away = other.transform.position - transform.position;
            enemyRb.AddForce(away * _force, ForceMode.Impulse);
            //playerAudio.PlayOneShot(crashSound, 1.0f);
            //explosionParticle.Play();
        }
    }
}
