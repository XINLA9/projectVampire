using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBullet : MonoBehaviour
{
    public GameObject bullet;
    // Initiate the movement speed and attackRange
    public float speed = 10f;
    public float attackRange = 10.0f;
    public GameObject nearestEnemy;
    public LayerMask enemiesToShoot;
    public bool shootCoolDown = true;
    private Rigidbody enemyRb;

    // Start is called before the first frame update
    void Start()
    {
        // Assign the rigidBody of enemy to variable
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        FindNearestEnemy();
        ShootNearestEnemy();
    }

    private void FindNearestEnemy()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, attackRange, enemiesToShoot);

        float closestDistance = Mathf.Infinity;
        if (enemies.Length > 0) {
            foreach (Collider enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.gameObject.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestEnemy = enemy.gameObject;
                }
            }
        } else {
            nearestEnemy = null;
        }
    }

    private void ShootNearestEnemy() {
        if (nearestEnemy != null) {
            Vector3 moveAway = (transform.position - nearestEnemy.transform.position).normalized;
            Vector3 shootDir = (nearestEnemy.transform.position - transform.position).normalized;
            enemyRb.AddForce(moveAway  * speed);
            if (shootCoolDown) {
                var newBullet = Instantiate(bullet, transform.position, bullet.transform.rotation);
                newBullet.GetComponent<Rigidbody>().AddForce(shootDir * speed);
                shootCoolDown = false;
                StartCoroutine(ReadyToShoot());
            }
        }
    }

    IEnumerator ReadyToShoot() {
        // Wait a second 
        yield return new WaitForSeconds(1);
        // Reset the cool down
        shootCoolDown = true;
    }
}

