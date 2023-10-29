using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Die : MonoBehaviour
{
    private Attributes _attributes;
    private Animator _animator;
    private bool _isDead;
    private float _HP;
    public bool isSkeleton; 
    public bool isWizard;
    public bool isDeadWizard;
    public bool isMushroom;
    public bool isBeholder;
    public bool isDragon;
    public GameObject speedUpCir;
    public GameObject poisonCir;
    public GameObject trappedCir;
    private NavMeshAgent _navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        _attributes = GetComponent<Attributes>();
        _animator = GetComponent<Animator>();
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _HP = _attributes.HP;
        _isDead = _attributes.isDead;  
        if (_HP <= 0 && !_isDead)
        {   
            if (isWizard) {
                ShootingBullet SB = GetComponent<ShootingBullet>();
                SB.enabled = false;
            }
            if (isDeadWizard) {
                SummonDead SD = GetComponent<SummonDead>();
                SD.enabled = false;
            }
            if (isBeholder) {
                Instantiate(trappedCir, transform.position, trappedCir.transform.rotation);
            }
            if (isMushroom) {
                Instantiate(speedUpCir, transform.position, speedUpCir.transform.rotation);
            }
            if (isDragon) {
                Instantiate(poisonCir, transform.position, poisonCir.transform.rotation);
            }
            Collider BC = gameObject.GetComponent<Collider>();
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            BC.isTrigger = true;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            rb.isKinematic = true;
            _attributes.isDead = true;
            _animator.SetBool("dead",true);
            _animator.SetTrigger("isDead");
            gameObject.tag = "Dead";
            GameObject uiElement = gameObject.transform.GetChild(0).gameObject;
            _navMeshAgent.speed = 0;
            uiElement.SetActive(false);
            int LayerIgnoreRaycast = LayerMask.NameToLayer("Dead");
            gameObject.layer = LayerIgnoreRaycast;
            if (isSkeleton) {
                StartCoroutine(DestorySkeleton());
            }
        }
    }

    IEnumerator DestorySkeleton() {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
    public void OnDeathAnimationEnd()
    {
        Destroy(gameObject);
    }
}
