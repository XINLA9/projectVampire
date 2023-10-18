using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseArrow : MonoBehaviour
{
    public GameObject Target;
    public float speed = 10.0f;
    private Rigidbody arrowRB;
    public float attack;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        arrowRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null) {
            if (GameObject.Find(Target.name) != null) {
                Vector3 moveDir = (Target.transform.position - transform.position).normalized;
                arrowRB.AddForce(moveDir * speed);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        ParticleSystem PS = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
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
