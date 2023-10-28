using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonLogic : MonoBehaviour
{
    private Rigidbody enemyRb;
    private Attributes attributes;
    private bool hasEnemy = false;
    private Animator skeletonAnim; 
    private bool EnemyAround = false;
    private GameObject nearestEnemy;
    private bool attackCD = false;
    private NavMeshAgent navAgent;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        attributes = GetComponent<Attributes>();
        skeletonAnim = GetComponent<Animator>();
        nearestEnemy = null;
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        FindNearestEnemy();
        if (hasEnemy && !EnemyAround) {
            RushToFight();
        }
        if (EnemyAround & !attackCD) {
            Debug.Log("Here");
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
        } else {
            nearestEnemy = null;
            EnemyAround = false;
        }
        if (closestDistance < attributes.attackRange) {
            EnemyAround = true;
        } else {
            EnemyAround = false;
        }
    }

    void RushToFight() {
        skeletonAnim.SetTrigger("hasEnemy");
        navAgent.destination = nearestEnemy.transform.position;
    }

    void Attack() {
        skeletonAnim.SetTrigger("canAttack");
        Attributes enemyAttri = nearestEnemy.GetComponent<Attributes>();
        enemyAttri.HP -= attributes.attack - enemyAttri.defense;
        StartCoroutine(AttackInCD());
    }

    IEnumerator AttackInCD() {
        attackCD = true;
        yield return new WaitForSeconds(4);
        attackCD = false;
    }
}
