using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour
{
    // Base Attributes
    public float HP_max;
    public float attack_base;
    public float ap_damage_base;
    public float defense_base;
    public float force_base;
    public float mass_base;

    // Current Attributes
    public float HP;
    public float attack;
    public float ap_damage;
    public float defense;
    public float force;
    public float mass;
<<<<<<< HEAD

    // Other Attributes
    public float maxSpeed;
    public float acceleration;
    public float rotationSpeed;
    public float attack_interval;
=======
    public float attackRange;
>>>>>>> 2d72cfb055e703fc416698f40132cb34a32264ec
    public bool isDead;
    public GameObject moveGoal;


    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        moveGoal = null;
        HP = HP_max;
        attack = attack_base;
        ap_damage = ap_damage_base;
        defense = defense_base;
        force = force_base;
        mass = mass_base;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP < 0) { HP = 0; }
    }
}
