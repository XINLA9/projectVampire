using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_die_action : MonoBehaviour
{
    private Unit_attributes _attributes;
    private Animator _animator;
    private float _HP;
    private bool _isDead;
    // Start is called before the first frame update
    void Start()
    {
        _attributes = GetComponent<Unit_attributes>();
        _animator = GetComponent<Animator>();
        _isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        _HP = _attributes.HP;
        if (_HP <= 0 && !_isDead)
        {
            _isDead = true;
            _animator.SetTrigger("isDead");
        }
    }
    public void OnDeathAnimationEnd()
    {
        Destroy(gameObject);
    }
}
