using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_attack_action : MonoBehaviour
{
    private Attributes _attributes;
    private Animator _animator;
    private int _attack;
    private float _force;
    private bool _isDead = false;
    public AudioClip attackSound;
    private bool _isAttacking;

    // The duration of the attack animation in seconds
    public float attackAnimationDuration = 1.0f; // Adjust this as needed
    // Start is called before the first frame update
    void Start()
    {
        _attributes = GetComponent<Attributes>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _attack = _attributes.attack;
        _force = _attributes.force;
        _isDead = _attributes.isDead;
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
            _animator.SetTrigger("attack"); 
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
        Attributes enemyState = enemy.GetComponent<Attributes>();
        enemyState.HP -= _attack;

        
        enemyRb.AddForce(-enemy.transform.forward * _force, ForceMode.Impulse);
        yield return new WaitForSeconds(attackAnimationDuration);
        _isAttacking = false;
        // Play attack sound if needed
        // playerAudio.PlayOneShot(attackSound, 1.0f);
    }
}
