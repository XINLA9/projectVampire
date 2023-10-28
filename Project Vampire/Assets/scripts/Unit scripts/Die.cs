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
