using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBullet : MonoBehaviour
{
    public GameObject bullet;
    // Initiate the movement speed and attackRange
    public float attackRange = 10.0f;
    public float rotationSpeed = 20.0f;
    private Attributes attributes;
    public GameObject nearestEnemy;
    public LayerMask enemiesToShoot;
    public bool shootCoolDown = true;
    private Rigidbody enemyRb;
    private Animator shooterAnim; 

    // Start is called before the first frame update
    void Start()
    {
        // Assign the rigidBody of enemy to variable
        enemyRb = GetComponent<Rigidbody>();
        shooterAnim = GetComponent<Animator>();
        attributes = GetComponent<Attributes>();
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
            shooterAnim.ResetTrigger("Enemy_detected");
            enemyRb.velocity = Vector3.zero;
        }
    }

    private void ShootNearestEnemy() {
        if (nearestEnemy != null) {
            Vector3 moveAway = (transform.position - nearestEnemy.transform.position).normalized;
            Vector3 shootDir = (nearestEnemy.transform.position - transform.position).normalized;
            Debug.Log("Here");
            Quaternion toRotation = Quaternion.LookRotation(shootDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, attributes.rotationSpeed * Time.deltaTime);
            shooterAnim.SetTrigger("Enemy_detected");
            enemyRb.AddForce(moveAway * attributes.maxSpeed);
            if (shootCoolDown) {
                shooterAnim.SetTrigger("Ready_fire");
                var newBullet = Instantiate(bullet, transform.GetChild(2).position, transform.rotation);
                Rigidbody rb = newBullet.GetComponent<Rigidbody>();
                rb.AddForce(shootDir * attributes.maxSpeed, ForceMode.Impulse);
                shootCoolDown = false;
                StartCoroutine(ReadyToShoot());
            }
        }
    }

    IEnumerator ReadyToShoot() {
        // Wait a second 
        yield return new WaitForSeconds(2);
        // Reset the cool down
        shootCoolDown = true;
    }
}

