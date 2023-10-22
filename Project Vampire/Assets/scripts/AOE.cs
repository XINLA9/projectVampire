using System;
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
        rescaleCircle(0);
        rescaleCircle(1);
        rescaleAccCir();
        if (heal) {
            GameObject healingCir = gameObject.transform.GetChild(0).gameObject;
            healingCir.SetActive(true);
        }
        if (slow) {
            GameObject slowingCir = gameObject.transform.GetChild(1).gameObject;
            slowingCir.SetActive(true);
        }
        if (speedUp) {
            GameObject speedingCir = gameObject.transform.GetChild(2).gameObject;
            speedingCir.SetActive(true);
        }
        // Change the size of effect

    }

    // Update is called once per frame
    void Update()
    {
        Collider[] units = Physics.OverlapSphere(transform.position, range, allUnits);
        foreach (Collider unit in units) {
            Rigidbody unitRb = unit.gameObject.GetComponent<Rigidbody>();
            Attributes unitAB = unit.gameObject.GetComponent<Attributes>();
            int pos = Array.IndexOf(colliders, unit);
            if (pos == -1) {
                if (!speedDictionary.ContainsKey(unit.gameObject.name)) {
                    speedDictionary.Add(unit.gameObject.name, unitRb.velocity);
                } else {
                    speedDictionary[unit.gameObject.name] = unitRb.velocity;
                }
            }
            if (heal) {
                unitAB.HP += healRate * Time.deltaTime;
            }
            if (slow) {
                unitAB.maxSpeed -= slowAmount;
            }
            if (speedUp) {
                unitAB.maxSpeed += speedUpAmount;
            }

        }
        foreach (Collider collider in colliders) {
            int pos = Array.IndexOf(units, collider);
            if (pos == -1) {
                resumeSpeed(collider);
            }
        }
        colliders = units;
    }

    public void resumeSpeed(Collider unit) {
        Rigidbody unitRb = unit.gameObject.GetComponent<Rigidbody>();
        unitRb.velocity = speedDictionary[unit.gameObject.name];
    }

    private void rescaleCircle(int circleNum) {
        ParticleSystem PS = gameObject.transform.GetChild(circleNum).gameObject.GetComponent<ParticleSystem>();
        var main = PS.main;
        main.startSize = range;
        ParticleSystem PSone = gameObject.transform.GetChild(circleNum).GetChild(0).gameObject.GetComponent<ParticleSystem>();
        var mainOne = PSone.main;
        mainOne.startSizeX = range/2;
        mainOne.startSizeZ = range/2;
        ParticleSystem PStwo = gameObject.transform.GetChild(circleNum).GetChild(2).gameObject.GetComponent<ParticleSystem>();
        var shapeTwo = PStwo.shape;
        shapeTwo.radius = range/2;
        ParticleSystem PSthree = gameObject.transform.GetChild(circleNum).GetChild(3).gameObject.GetComponent<ParticleSystem>();
        var shapeThree = PSthree.shape;
        shapeThree.radius = range/2;
    }

    private void rescaleAccCir() {
        ParticleSystem PS = gameObject.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();
        var main = PS.main;
        main.startSize = range;
        ParticleSystem PSone = gameObject.transform.GetChild(2).GetChild(0).gameObject.GetComponent<ParticleSystem>();
        var shapeOne = PSone.shape;
        shapeOne.radius = range/2;
        ParticleSystem PStwo = gameObject.transform.GetChild(2).GetChild(1).gameObject.GetComponent<ParticleSystem>();
        var shapeTwo = PStwo.shape;
        shapeTwo.radius = range/2;
        ParticleSystem PSthree = gameObject.transform.GetChild(2).GetChild(2).gameObject.GetComponent<ParticleSystem>();
        var mainThree = PSthree.main;
        mainThree.startSize = range/2;
        ParticleSystem PSfour = gameObject.transform.GetChild(2).GetChild(3).gameObject.GetComponent<ParticleSystem>();
        var shapefour = PSfour.shape;
        shapefour.radius = range/2;
    }
}
