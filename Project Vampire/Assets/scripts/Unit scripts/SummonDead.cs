using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        attributes = GetComponent<Attributes>();
        SB = GetComponent<ShootingBullet>();
        wizardAnim = GetComponent<Animator>();
        nearestDead = null;
        magicCircle = transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();
        magicCircle.Stop();
    }

    // Update is called once per frame
    void Update()
    {   
        FindNearestDead();
        MoveToNearestDead();
        if (summonReady && haveDead) {
            SummonSkeleton();
        }
    }

    void SummonSkeleton() {
        enemyRb.velocity = Vector3.zero;
        enemyRb.isKinematic = true;
        summonStart = true;
        wizardAnim.SetTrigger("summonDead");
        StartCoroutine(animCoroutine());
        StartCoroutine(StartCD());
    }

    IEnumerator animCoroutine() {
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
        bool canMove = (summonReady && !haveDead) || (!summonReady);
        if (canMove && !(nearestDead == null) && (SB.nearestEnemy == null) && !summonStart) {
            wizardAnim.SetTrigger("haveDead");
            Vector3 moveDir = (nearestDead.transform.position - transform.position).normalized;
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, attributes.rotationSpeed * Time.deltaTime);
            enemyRb.AddForce(moveDir * attributes.maxSpeed);
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
        } 
        if (leastDistance < summonRange) {
            haveDead = true;
        }
    }
}
