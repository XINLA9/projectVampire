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
        bool isSkeleton = gameObject.name == "Big_Skeleton";
        if (_HP <= 0 && !_isDead && !isSkeleton)
        {
            Collider BC = gameObject.GetComponent<Collider>();
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            BC.isTrigger = true;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            rb.isKinematic = true;
            _attributes.isDead = true;
            _animator.SetTrigger("isDead");
            gameObject.tag = "Dead";
            int LayerIgnoreRaycast = LayerMask.NameToLayer("Dead");
            gameObject.layer = LayerIgnoreRaycast;
        }
        if (_HP <= 0 && !_isDead && isSkeleton) {
            Destroy(gameObject);
        }
    }
    public void OnDeathAnimationEnd()
    {
        Destroy(gameObject);
    }
}
