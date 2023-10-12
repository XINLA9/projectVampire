using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private Attributes _attributes;
    private Animator _animator;
    private float _force;
    private float _attack_interval;
    private bool _isDead = false;
    public AudioClip attackSound;
    private bool _isAttacking;

    // The duration of the attack animation in seconds
    public float attackAnimationDuration = 0.5f; 
    // Start is called before the first frame update
    void Start()
    {
        _attributes = GetComponent<Attributes>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _force = _attributes.force;
        _isDead = _attributes.isDead;
        _attack_interval = _attributes.attack_interval;
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
        // If enemy collides with either goal, destroy it
        if (other.gameObject.CompareTag(enemyTag) && !_isDead && !_isAttacking)
        {
            _isAttacking = true;
            if (!_isDead)
            {
            _animator.SetTrigger("attack"); 
            }
            // Start a coroutine to wait for the attack animation to finish
            StartCoroutine(DealDamageAfterAnimation(other.gameObject));
        }
    }

    // Coroutine to deal damage after the attack animation
    private IEnumerator DealDamageAfterAnimation(GameObject enemy)
    {
        // Wait for the attack animation duration
        yield return new WaitForSeconds(attackAnimationDuration);

        // Deal damage to the enemy
        Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
        Attributes e_attributes = enemy.GetComponent<Attributes>();
        e_attributes.HP -= _attributes.ap_damage;
        float attack = Math.Max(0, _attributes.attack - e_attributes.defense);
        e_attributes.HP -= attack;

        Vector3 back = enemy.transform.forward;
        back.y = 0;
        back = back.normalized;

        float pushDuration = 0.5f; 
        float startTime = Time.time;
        while (Time.time - startTime < pushDuration)
        {
            enemyRb.AddForce(-back * (_force - e_attributes.mass), ForceMode.VelocityChange);
            yield return null;
        }
        yield return new WaitForSeconds(_attack_interval);

        _isAttacking = false;
        // Play attack sound if needed
        // playerAudio.PlayOneShot(attackSound, 1.0f);
    }
}
