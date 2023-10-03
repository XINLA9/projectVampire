using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour
{
    public int HP;
    public int attack;
    public int defense;
    public float acceleration;
    public float maxSpeed;
    public float force;
    public float rotationSpeed;
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
        
    }
}
