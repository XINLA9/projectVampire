using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE : MonoBehaviour
{
    public float range = 10.0f;
    public bool heal;
    public float healRate;
    public bool slow;
    public float slowAmount;
    public bool poison;
    public float poisonRate;
    public bool speedUp;
    public float speedUpAmount;
    public bool trapped;
    public int trappedSeconds;
    private Dictionary<string, Vector3> speedDictionary; 
    public LayerMask allUnits;
    public Collider[] colliders;
    // Start is called before the first frame update
    void Start()
    {
        Collider[] units = Physics.OverlapSphere(transform.position, range, allUnits);
        foreach (Collider unit in units) {
            Rigidbody unitRb = unit.gameObject.GetComponent<Rigidbody>();
            speedDictionary.Add(unit.gameObject.name, unitRb.velocity);
        }
        colliders = units;
        ParticleSystem PS = gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        // Change the size of effect

    }

    // Update is called once per frame
    void Update()
    {
        Collider[] units = Physics.OverlapSphere(transform.position, range, allUnits);
    }

    public void resumeSpeed(Collider unit) {
        
    }
}
