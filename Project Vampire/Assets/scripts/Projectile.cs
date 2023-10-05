using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float mapBound = 60.0f;
    public float speed;
    public int attack;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Translate(transform.forward * Time.deltaTime * speed);
        removeOfBound();
    }
    private void removeOfBound()
    {
        if (transform.position.x > mapBound)
        {
            Destroy(gameObject);
        }
        if (transform.position.x < -mapBound)
        {
            Destroy(gameObject);
        }
        if (transform.position.z > mapBound)
        {
            Destroy(gameObject);
        }
        if (transform.position.z < -mapBound)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("monster"))
        {
            Destroy(gameObject);
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
            Units enemyState = other.gameObject.GetComponent<Units>();
            int damage = attack - enemyState.defense;
            if (damage > 0)
            {
                enemyState.HP -= damage;
            }
            Vector3 away = other.transform.position - transform.position;
            enemyRb.AddForce(away * force, ForceMode.Impulse);
        }
        
        if (other.gameObject.CompareTag("hunter")) {
            Destroy(gameObject);
        }
    }
}
