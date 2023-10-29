using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootingBullet : MonoBehaviour
{
    public GameObject bullet;
    // Initiate the movement current_speed and attackRange
    public float attentionRange = 10.0f;
    public float attackRange = 10.0f;
    private Attributes attributes;
    public GameObject nearestEnemy;
    public GameObject targetEnemy;
    public GameObject injuredAlly;
    public LayerMask enemiesToShoot;
    public bool shootCoolDown = true;
    private Rigidbody enemyRb;
    private Animator shooterAnim; 
    public float distanceTol = 1.0f;
    public float attackCoolDown = 2.0f;
    public float healingRange = 15.0f;
    public Vector3 moveAway;
    public Vector3 moveDir;
    public bool aggressive = false;
    public bool isPriest;
    public bool isDeathWiz;
    private NavMeshAgent navAgent;
    private bool healReady = true;
    public GameObject healingCircle;
    // Start is called before the first frame update
    void Start()
    {
        // Assign the rigidBody of enemy to variable
        enemyRb = GetComponent<Rigidbody>();
        shooterAnim = GetComponent<Animator>();
        attributes = GetComponent<Attributes>();
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = attributes.maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!attributes.isDead){
            if (!aggressive) {
                if (isPriest) {
                    ChaseNearestAlly();
                }
                FindNearestEnemy();
                ShootNearestEnemy();
            } else {
                if (nearestEnemy == null) {
                    ChaseNearestEnemy();
                }
                FindNearestEnemy();
                ShootNearestEnemy();
            }
        }
    }
    
    private void ChaseNearestAlly() {
        string targetTag = null;
        if (gameObject.tag == "monster") {
            targetTag = "monster";
        } 
        if (gameObject.tag == "hunter") {
            targetTag = "hunter";
        }
        GameObject[] targetUnits = GameObject.FindGameObjectsWithTag(targetTag);
        float leastDistance = Mathf.Infinity;
        if (targetUnits.Length > 0) {
            foreach (GameObject target in targetUnits) {
                Attributes allyAttri = target.GetComponent<Attributes>();
                bool injured = allyAttri.HP < allyAttri.HP_max && !allyAttri.isDead;
                if (injured) {
                    float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
                    if (distance < leastDistance) {
                        leastDistance = distance;
                        injuredAlly = target;
                    }
                }
            }
        } else {
            injuredAlly = null;
            shooterAnim.ResetTrigger("Desire_enemy");
        }
        if (injuredAlly != null) {
            shooterAnim.SetTrigger("Desire_enemy");
            navAgent.destination = injuredAlly.transform.position;
            navAgent.stoppingDistance = healingRange - 5;
            moveDir = (injuredAlly.transform.position - transform.position).normalized;
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, attributes.rotationSpeed * Time.deltaTime);
            if (leastDistance < healingRange && healReady) {
                shooterAnim.SetTrigger("StartHeal");
                navAgent.destination = transform.position;
                StartCoroutine(ResetAnimator());
                StartCoroutine(StartCD());
            }
        }
    }

    IEnumerator StartCD() {
        healReady = false;
        yield return new WaitForSeconds(10);
        healReady = true;
    }

    IEnumerator ResetAnimator() {
        yield return new WaitForSeconds(3);
        shooterAnim.ResetTrigger("StartHeal");
    }

    private void ChaseNearestEnemy() {
        string targetTag = null;
        if (gameObject.tag == "monster") {
            targetTag = "hunter";
        } 
        if (gameObject.tag == "hunter") {
            targetTag = "monster";
        }
        GameObject[] targetUnits = GameObject.FindGameObjectsWithTag(targetTag);
        float leastDistance = Mathf.Infinity;
        if (targetUnits.Length > 0) {
            foreach (GameObject target in targetUnits) {
                float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
                if (distance < leastDistance) {
                    leastDistance = distance;
                    targetEnemy = target;
                }
            }
        } else {
            targetEnemy = null;
            shooterAnim.ResetTrigger("Desire_enemy");
        }
        if (targetEnemy != null) {
            shooterAnim.SetTrigger("Desire_enemy");
            if (leastDistance < attackRange) {
                navAgent.destination = transform.position;
            } else {
                navAgent.destination = targetEnemy.transform.position;
                navAgent.stoppingDistance = attackRange;
            }
            moveDir = (targetEnemy.transform.position - transform.position).normalized;
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, attributes.rotationSpeed * Time.deltaTime);
        }
    }
    private void FindNearestEnemy()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, attentionRange, enemiesToShoot);

        float closestDistance = Mathf.Infinity;
        //Debug.Log("THe number of enemy is " + enemies.Length);
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
            bool keepPriest = isPriest && (injuredAlly == null);
            bool keepAggre = aggressive && (targetEnemy == null);
            bool keepDeathWiz = isDeathWiz && (GetComponent<SummonDead>().nearestDead == null);
            bool notPriNotAgg = !isPriest && (!aggressive) && (!isDeathWiz);
            if (keepPriest || keepAggre || notPriNotAgg || keepDeathWiz) {
                navAgent.destination = transform.position;
            }
        }
    }

    private void ShootNearestEnemy() {
        if (nearestEnemy != null) {
            navAgent.destination = transform.position;
            enemyRb.velocity = Vector3.zero;
            moveAway = (transform.position - nearestEnemy.transform.position).normalized;
            Vector3 shootDir = (nearestEnemy.transform.position - transform.position).normalized;
            Quaternion toRotation = Quaternion.LookRotation(shootDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, attributes.rotationSpeed * Time.deltaTime);
            shooterAnim.SetTrigger("Enemy_detected");
            float disToNearestEnemy = Vector3.Distance(transform.position, nearestEnemy.transform.position);
            if (Mathf.Abs(disToNearestEnemy - attentionRange) < distanceTol) {
                enemyRb.velocity = Vector3.zero;
            } else {
                enemyRb.AddForce(moveAway * attributes.maxSpeed);
            }
            
            if (shootCoolDown && (disToNearestEnemy < attackRange)) {
                shooterAnim.SetTrigger("Ready_fire");
                shootCoolDown = false;
                StartCoroutine(ReadyToShoot());
            }
        }
        if (targetEnemy != null) {
            float disToTargetEnemy = Vector3.Distance(transform.position, targetEnemy.transform.position);
            if (shootCoolDown && (disToTargetEnemy < attackRange)) {
                shooterAnim.SetTrigger("Ready_fire");
                shootCoolDown = false;
                StartCoroutine(ReadyToShoot());
            }
        }
    }

    IEnumerator ReadyToShoot() {
        // Wait a second 
        yield return new WaitForSeconds(attackCoolDown);
        // Reset the cool down
        shootCoolDown = true;
    }

    public Attributes GetAttributes() {
        return attributes;
    }
}

