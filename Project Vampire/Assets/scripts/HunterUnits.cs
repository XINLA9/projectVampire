using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterUnits : MonoBehaviour
{
    // attribute of this units
    public int HP;
    public int attack;
    public int defense;
    public float acceleration;
    public float maxSpeed;
    public float force;
    public float mapBound = 25.0f;
    public bool isHunter;
    public float rotationSpeed = 5.0f;

    // attribute for range units
    public bool isRange;
    public float minDisToEnemy;
    public GameObject projectile;
    public float shootInterval = 2.0f;

    private Rigidbody rb;
    private string enemyTag;
    private GameObject nearestEnemy;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (isHunter)
        {
            enemyTag = "monster";
        }
        else
        {
            enemyTag = "hunter";
        }
        if (isRange)
        {
            StartCoroutine(ShootArrows());
        }
    }

    // Update is called once per frame
    void Update()
    {
        FindNearestEnemy();
        if (isRange)
        {
            statAwayFromEnemy();
        }
        else
        {
            MoveTowardsNearestHunter();
        }
        remove();
        stayInMap();
    }

    private void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestEnemy = enemy;
            }
        }
    }
    // move logic for melee units, there will straight forward to the enemy
    private void MoveTowardsNearestHunter()
    {
        if (nearestEnemy != null)
        {
            Vector3 lookDirection = (nearestEnemy.transform.position - transform.position).normalized;
            //lookDirection.y = 0f;

            // Calculate the target direction of rotation using Quaternion.LookRotation
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

            // Rotate the unit to face the target direction
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            Vector3 targetVelocity = lookDirection * maxSpeed;

            Vector3 velocityDiff = targetVelocity - rb.velocity;

            Vector3 accelerationVec = velocityDiff / Time.fixedDeltaTime;

            accelerationVec = Vector3.ClampMagnitude(accelerationVec, this.acceleration);
            //accelerationVec.y = 0f;

            rb.AddForce(accelerationVec, ForceMode.Impulse);
        }
    }
    // move logic for range units, there will keep a distance with the enemy
    private void statAwayFromEnemy()
    {
        if (nearestEnemy != null)
        {
 
            Vector3 lookDirection = (nearestEnemy.transform.position - transform.position).normalized;

            //lookDirection.y = 0f;

            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            float distanceToEnemy = Vector3.Distance(transform.position, nearestEnemy.transform.position);

            if (distanceToEnemy < minDisToEnemy)
            {
                Vector3 targetVelocity = lookDirection * maxSpeed;

                Vector3 velocityDiff = targetVelocity - rb.velocity;

                Vector3 accelerationVec = velocityDiff / Time.fixedDeltaTime;

                accelerationVec = Vector3.ClampMagnitude(accelerationVec, this.acceleration);
                accelerationVec.y = 0f;

                rb.AddForce(-accelerationVec, ForceMode.Impulse);
            }
        }
    }
    private IEnumerator ShootArrows()
    {
        while (true) 
        {
            yield return new WaitForSeconds(shootInterval);
            if (nearestEnemy != null)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, nearestEnemy.transform.position);
                if (distanceToEnemy > minDisToEnemy)
                {
                Vector3 shootingDirection = transform.forward;

                Vector3 projectilePos = transform.position + shootingDirection * 2;


                GameObject arrow = Instantiate(projectile, projectilePos, Quaternion.identity);

                arrow.transform.rotation = Quaternion.LookRotation(shootingDirection);
                }
            }    
        }
    }
    // if the unit HP is beyond 0 ,destroy it
    private void remove()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
    // prevent the units from leaving the map
    private void stayInMap()
    {
        if(transform.position.x > mapBound) {
            transform.position =  new Vector3(mapBound - 0.5f, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -mapBound)
        {
            transform.position = new Vector3(-mapBound + 0.5f, transform.position.y, transform.position.z);
        }
        if (transform.position.z > mapBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, mapBound - 0.5f);
        }
        if (transform.position.z < -mapBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -mapBound + 0.5f);
        }
        //if (transform.position.y < 0)
        //{
        //    transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        //}
        //if (transform.position.y > 5)
        //{
        //    transform.position = new Vector3(transform.position.x, 5.0f, transform.position.z);
        //}
        //if (transform.rotation.y != 0)
        //{
        //    transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        //}
    }
    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.CompareTag(enemyTag))
        {
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
           Units enemyState = other.gameObject.GetComponent<Units>();
            int damage = attack - enemyState.defense;
            if (damage > 0)
            {
                enemyState.HP -= damage;
            }
            Vector3 away = other.transform.position - transform.position;
            enemyRb.AddForce(away * force ,ForceMode.Impulse);
        }
    }
}

