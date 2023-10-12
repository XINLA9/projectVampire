using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour
{
    public float HP;
    public float HP_max;
    public float attack;
    public float armor_piercing_damage;
    public float defense;
    public float acceleration;
    public float maxSpeed;
    public float force;
    public float rotationSpeed;
    public float mass;
    public float attackRange;
    public bool isDead;
    public GameObject moveGoal;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        moveGoal = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP < 0) { HP = 0; }
    }
}
