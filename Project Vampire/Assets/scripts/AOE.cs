using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE : MonoBehaviour
{
    public float range = 10.0f;
    public bool heal;
    public float healRate;
    public bool slow;
    public float slowRate;
    public bool poison;
    public float poisonRate;
    public bool speedUp;
    public float speedUpRate;
    public bool trapped;
    public int trappedSeconds;
    public LayerMask allUnits;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] units = Physics.OverlapSphere(transform.position, range, allUnits);
        foreach (Collider unit in units) {
            if (trapped) {
                StartCoroutine(trapUnits(unit));
            }
            Attributes attri = unit.gameObject.GetComponent<Attributes>();

        }
    }

    IEnumerator trapUnits(Collider unit) {
        Rigidbody unitRb = unit.gameObject.GetComponent<Rigidbody>();
        Vector3 oriVelocity = unitRb.velocity;
        unitRb.velocity = Vector3.zero;
        yield return new WaitForSeconds(trappedSeconds);
        unitRb.velocity = oriVelocity;
    }
}
