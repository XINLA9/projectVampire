using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonLogic : MonoBehaviour
{
    private Rigidbody enemyRb;
    private Attributes attributes;
    private bool hasEnemy = false;
    private Animator skeletonAnim; 
    private bool EnemyAround = false;
    private GameObject nearestEnemy;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        attributes = GetComponent<Attributes>();
        skeletonAnim = GetComponent<Animator>();
        nearestEnemy = null;
    }

    // Update is called once per frame
    void Update()
    {
        FindNearestEnemy();
        if (hasEnemy && !EnemyAround) {
            RushToFight();
        }
        if (EnemyAround) {
            enemyRb.velocity = Vector3.zero;
            Attack();
        }
    }

    void FindNearestEnemy() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("monster");
        float closestDistance = Mathf.Infinity;
        if (enemies.Length > 0) {
            hasEnemy = true;
            foreach (GameObject enemy in enemies) {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }
        if (closestDistance < attributes.attackRange) {
            EnemyAround = true;
        }
    }

    void RushToFight() {
        skeletonAnim.SetTrigger("hasEnemy");
        Vector3 moveDir = (nearestEnemy.transform.position - transform.position).normalized;
        enemyRb.AddForce(moveDir * attributes.maxSpeed);
        Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, attributes.rotationSpeed * Time.deltaTime);
    }

    void Attack() {
        skeletonAnim.SetTrigger("canAttack");
    }
}
