using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float mapBound = 60.0f;
    public float speed;
    public float attack;
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
        ParticleSystem PS = transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
        PS.Stop();
        Destroy(gameObject);
        if (other.gameObject.CompareTag("monster"))
        {
            Attributes enemyAttributes = other.gameObject.GetComponent<Attributes>();
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
            float damage = attack - enemyAttributes.defense;
            if (damage > 0)
            {
                enemyAttributes.HP -= damage;
            }
            Vector3 away = (other.transform.position - transform.position).normalized;
            enemyRb.AddForce(away * force, ForceMode.Impulse);
        }
    }
}
