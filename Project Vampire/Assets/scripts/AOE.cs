using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AOE : MonoBehaviour
{
    public float range = 10.0f;
    public bool heal;
    public float healRate;
    public bool slow;
    public float slowRate;
    public bool poison;
    public float dmgRate;
    public bool speedUp;
    public float speedUpRate;
    public bool trapped;
    private Dictionary<string, float> speedDictionary = new Dictionary<string, float>();
    public LayerMask allUnits;
    public Collider[] colliders;
    public int existTime;
    private float PSrange;
    // Start is called before the first frame update
    void Start()
    {
        PSrange = range * 2;
        Collider[] units = Physics.OverlapSphere(transform.position, range, allUnits);
        if (units.Length > 0) {
            foreach (Collider unit in units) {
                NavMeshAgent unitNMA = unit.gameObject.GetComponent<NavMeshAgent>();
                speedDictionary.Add(unit.gameObject.name, unitNMA.speed);
            }
        }
        colliders = units;
        rescaleCircle(0);
        rescaleCircle(1);
        rescaleAccCir();
        rescaleDmgCir();
        rescaleTrapCir();
        if (heal) {
            activeCircle(0);
        }
        if (slow) {
            activeCircle(1);
        }
        if (speedUp) {
            activeCircle(2);
        }
        if (trapped) {
            activeCircle(3);
        }
        if (poison) {
            activeCircle(4);
        }
        // Change the size of effect
        StartCoroutine(EndAOE());
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] units = Physics.OverlapSphere(transform.position, range, allUnits);
        if (units.Length > 0) {
            foreach (Collider unit in units) {
                NavMeshAgent unitNMA = unit.gameObject.GetComponent<NavMeshAgent>();
                Attributes unitAB = unit.gameObject.GetComponent<Attributes>();
                int pos = Array.IndexOf(colliders, unit);
                if (pos == -1) {
                    if (!speedDictionary.ContainsKey(unit.gameObject.name)) {
                        speedDictionary.Add(unit.gameObject.name, unitNMA.speed);
                    } else {
                        speedDictionary[unit.gameObject.name] = unitNMA.speed;
                    }
                }

                if (heal) {
                    unitAB.HP += healRate * Time.deltaTime;
                }
                if (slow) {
                    unitNMA.speed = (1 - slowRate) * speedDictionary[unit.gameObject.name];
                }
                if (speedUp) {
                    unitNMA.speed = (1 + speedUpRate) * speedDictionary[unit.gameObject.name];
                }
                if (trapped) {
                    unitNMA.speed = 0;
                }
                if (poison) {
                    unitAB.HP -= dmgRate * Time.deltaTime;
                }
            }
        }
        foreach (Collider collider in colliders) {
            int pos = Array.IndexOf(units, collider);
            if (pos == -1 && (slow || speedUp || trapped)) {
                resumeSpeed(collider);
            }
        }

        colliders = units;
    }

    
    private void activeCircle(int index) {
        GameObject circle = transform.GetChild(index).gameObject;
        circle.SetActive(true);
        ParticleSystem PS = circle.GetComponent<ParticleSystem>();
        PS.Play();
    }

    public void resumeSpeed(Collider unit) {
        NavMeshAgent unitNMA = unit.gameObject.GetComponent<NavMeshAgent>();
        unitNMA.speed = speedDictionary[unit.gameObject.name];
    }


    private void rescaleCircle(int circleNum) {
        ParticleSystem PS = gameObject.transform.GetChild(circleNum).gameObject.GetComponent<ParticleSystem>();
        var main = PS.main;
        main.startSize = PSrange;
        main.startLifetime = existTime;
        ParticleSystem PSone = gameObject.transform.GetChild(circleNum).GetChild(0).gameObject.GetComponent<ParticleSystem>();
        var mainOne = PSone.main;
        mainOne.startSizeX = PSrange/2;
        mainOne.startSizeZ = PSrange/2;
        ParticleSystem PStwo = gameObject.transform.GetChild(circleNum).GetChild(2).gameObject.GetComponent<ParticleSystem>();
        var shapeTwo = PStwo.shape;
        shapeTwo.radius = PSrange/2;
        ParticleSystem PSthree = gameObject.transform.GetChild(circleNum).GetChild(3).gameObject.GetComponent<ParticleSystem>();
        var shapeThree = PSthree.shape;
        shapeThree.radius = PSrange/2;
    }

    private void rescaleAccCir() {
        ParticleSystem PS = gameObject.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();
        var main = PS.main;
        main.startSize = PSrange;
        main.startLifetime = existTime;
        ParticleSystem PSone = gameObject.transform.GetChild(2).GetChild(0).gameObject.GetComponent<ParticleSystem>();
        var shapeOne = PSone.shape;
        shapeOne.radius = PSrange/2;
        ParticleSystem PStwo = gameObject.transform.GetChild(2).GetChild(1).gameObject.GetComponent<ParticleSystem>();
        var shapeTwo = PStwo.shape;
        shapeTwo.radius = PSrange/2;
        ParticleSystem PSthree = gameObject.transform.GetChild(2).GetChild(2).gameObject.GetComponent<ParticleSystem>();
        var mainThree = PSthree.main;
        mainThree.startSize = PSrange/2;
        ParticleSystem PSfour = gameObject.transform.GetChild(2).GetChild(3).gameObject.GetComponent<ParticleSystem>();
        var shapefour = PSfour.shape;
        shapefour.radius = PSrange/2;
    }

    private void rescaleTrapCir() {
        ParticleSystem PS = gameObject.transform.GetChild(3).gameObject.GetComponent<ParticleSystem>();
        var main = PS.main;
        main.startSize = PSrange;
        main.startLifetime = existTime;
        ParticleSystem PSone = gameObject.transform.GetChild(3).GetChild(0).gameObject.GetComponent<ParticleSystem>();
        var shapeOne = PSone.shape;
        shapeOne.radius = PSrange/2;
        ParticleSystem PStwo = gameObject.transform.GetChild(3).GetChild(1).gameObject.GetComponent<ParticleSystem>();
        var mainTwo = PStwo.main;
        mainTwo.startSize = PSrange;
    }

    private void rescaleDmgCir() {
        ParticleSystem PS = gameObject.transform.GetChild(4).gameObject.GetComponent<ParticleSystem>();
        var main = PS.main;
        main.startSize = PSrange;
        main.startLifetime = existTime;
        ParticleSystem PSone = gameObject.transform.GetChild(4).GetChild(0).gameObject.GetComponent<ParticleSystem>();
        var shapeOne = PSone.shape;
        shapeOne.radius = PSrange/2;
        ParticleSystem PStwo = gameObject.transform.GetChild(4).GetChild(1).gameObject.GetComponent<ParticleSystem>();
        var shapeTwo = PStwo.shape;
        shapeTwo.radius = PSrange/2;
    }

    IEnumerator EndAOE() {
        yield return new WaitForSeconds(existTime);
        Collider[] units = Physics.OverlapSphere(transform.position, range, allUnits);
        if (units.Length > 0) {
            foreach (Collider unit in units) {
                resumeSpeed(unit);
            }
        }
        Destroy(gameObject);
    }
}
