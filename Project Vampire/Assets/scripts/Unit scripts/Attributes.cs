using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour
{
    public float HP;
    public float HP_max;
    public float attack_base;
    public float attack;
    public float ap_damage;
    public float defense_base;
    public float defense;
    public float acceleration;
    public float maxSpeed_base;
    public float maxSpeed;
    public float force_base;
    public float force;
    public float rotationSpeed;
    public float mass_base;
    public float mass;
    public float attack_interval;
    public bool isDead;
    public GameObject moveGoal;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        moveGoal = null;
        HP = HP_max;
        attack = attack_base;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP < 0) { HP = 0; }
    }
}
