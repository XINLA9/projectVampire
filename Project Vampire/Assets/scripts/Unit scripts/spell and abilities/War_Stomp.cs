using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class War_Stomp : MonoBehaviour
{
    private Animator _animator;
    private Attributes _attributes;
    public float manaCost;
    public float coolDown;
    public float damage;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _attributes = GetComponent<Attributes>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
