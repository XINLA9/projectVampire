using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attributes : MonoBehaviour
{
    // Base Attributes
    public float HP_max;
    public float mana_max;
    public float attack_base;
    public float ap_damage_base;
    public float defense_base;
    public float maxSpeed;
    public float acceleration;
    public float rotationSpeed;
    public float attack_interval;
    public float attackRange;
    public string description = "d";

    public Sprite profolio;

    // Current Attributes
    public float HP;
    public float mana;
    public float attack;
    public float ap_damage;
    public float defense;
    public bool isDead;
    public GameObject moveGoal;


    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        moveGoal = null;
        HP = HP_max;
        mana = mana_max;
        attack = attack_base;
        ap_damage = ap_damage_base;
        defense = defense_base;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP < 0) { HP = 0; }
    }
}
