using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    private Attributes _attributes;
    private Animator _animator;
    private bool _isDead;
    private float _HP;
    // Start is called before the first frame update
    void Start()
    {
        _attributes = GetComponent<Attributes>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _HP = _attributes.HP;
        _isDead = _attributes.isDead;  
        if (_HP <= 0 && !_isDead)
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            rb.isKinematic = true;
            Collider collider = rb.GetComponent<Collider>();
            collider.enabled = false;
            _attributes.isDead = true;
            _animator.SetTrigger("isDead");
        }
    }
    public void OnDeathAnimationEnd()
    {
        Destroy(gameObject);
    }
}
