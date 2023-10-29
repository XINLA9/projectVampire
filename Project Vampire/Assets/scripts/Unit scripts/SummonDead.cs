using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SummonDead : MonoBehaviour
{
    public float summonRange = 35.0f;
    public float summonCoolDown = 10.0f;
    public GameObject nearestDead;
    private bool summonReady = true;
    public GameObject[] skeletonList;
    private Rigidbody enemyRb;
    private Attributes attributes;
    private ShootingBullet SB;
    private bool haveDead = false;
    private Animator wizardAnim; 
    private bool summonStart = false;
    private ParticleSystem magicCircle;
    public LayerMask deadUnits;
    public GameObject skeleton_1;
    private NavMeshAgent navAgent;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        attributes = GetComponent<Attributes>();
        SB = GetComponent<ShootingBullet>();
        wizardAnim = GetComponent<Animator>();
        nearestDead = null;
        magicCircle = transform.GetChild(4).gameObject.GetComponent<ParticleSystem>();
        magicCircle.Stop();
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = attributes.maxSpeed;
        navAgent.stoppingDistance = summonRange;
    }

    // Update is called once per frame
    void Update()
    {   
        if (!attributes.isDead) {
            FindNearestDead();
            MoveToNearestDead();
            if (summonReady && haveDead) {
                SummonSkeleton();
            }
        }
    }

    void SummonSkeleton() {
        enemyRb.velocity = Vector3.zero;
        enemyRb.isKinematic = true;
        summonStart = true;
        wizardAnim.SetTrigger("summonDead");
        StartCoroutine(convertDeadToSkeleton());
        StartCoroutine(AnimCoroutine());
        StartCoroutine(StartCD());
    }

    IEnumerator convertDeadToSkeleton() {
        yield return new WaitForSeconds(2);
        Collider[] deads = Physics.OverlapSphere(transform.position, summonRange, deadUnits);
        foreach (Collider dead in deads) {
            Die dieState = dead.gameObject.GetComponent<Die>();
            if (!dieState.isSkeleton) {
                var newSkeleton = Instantiate(skeleton_1, dead.gameObject.transform.position, dead.gameObject.transform.rotation);
                ParticleSystem summonEffect = newSkeleton.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
                summonEffect.Play();
                GameObject body = newSkeleton.transform.GetChild(1).gameObject;
                StartCoroutine(RevealBody(body));
                Destroy(dead.gameObject);
            // TODO:Need to bounce back any gameObject near the summoning area
            }
        }
    }

    IEnumerator RevealBody(GameObject body) {
        yield return new WaitForSeconds(4);
        body.SetActive(true);
    }

    IEnumerator AnimCoroutine() {
        yield return new WaitForSeconds(6);
        wizardAnim.SetTrigger("summonAnimEnd");
        summonStart = false;
    }

    IEnumerator StartCD() {
        summonReady = false;
        yield return new WaitForSeconds(summonCoolDown);
        summonReady = true;
    }

    void MoveToNearestDead() {
        if (!(nearestDead == null) && (SB.nearestEnemy == null) && !summonStart) {
            wizardAnim.SetTrigger("haveDead");
            Vector3 moveDir = (nearestDead.transform.position - transform.position).normalized;
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, attributes.rotationSpeed * Time.deltaTime);
            if (Vector3.Distance(nearestDead.transform.position, transform.position) < summonRange) {
                navAgent.destination = transform.position;
            } else {
                navAgent.destination = nearestDead.transform.position;
            }
        }
    }

    void FindNearestDead() {
        GameObject[] deadUnits = GameObject.FindGameObjectsWithTag("Dead");
        float leastDistance = Mathf.Infinity;
        if (deadUnits.Length > 0) {
            foreach (GameObject deads in deadUnits) {
                float distance = Vector3.Distance(gameObject.transform.position, deads.transform.position);
                if (distance < leastDistance) {
                    leastDistance = distance;
                    nearestDead = deads;
                }
            }
        } else {
            nearestDead = null;
            navAgent.destination = transform.position;
        }
        if (leastDistance < summonRange) {
            haveDead = true;
        }
    }
}
