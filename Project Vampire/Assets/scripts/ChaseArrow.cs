using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseArrow : MonoBehaviour
{
    public GameObject Target;
    public float speed = 1.0f;
    private Rigidbody arrowRB;
    public float attack;
    public float force;
    public Vector3 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        arrowRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null) {
            moveDir = (Target.transform.position - transform.position).normalized;
            Vector3 moveSpeed = moveDir * speed;
            Vector3 actualMove = new Vector3(moveSpeed.x, moveSpeed.y + 1, moveSpeed.z);
            arrowRB.AddForce(actualMove);
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
