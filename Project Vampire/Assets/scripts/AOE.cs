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
    public float slowRate;
    public bool poison;
    public float dmgRate;
    public bool speedUp;
    public float speedUpRate;
    public bool trapped;
    private Dictionary<string, Vector3> speedDictionary = new Dictionary<string, Vector3>();
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
                Rigidbody unitRb = unit.gameObject.GetComponent<Rigidbody>();
                speedDictionary.Add(unit.gameObject.name, unitRb.velocity);
            }
        }
        colliders = units;
        rescaleCircle(0);
        rescaleCircle(1);
        rescaleAccCir();
        rescaleDmgCir();
        rescaleTrapCir();
        if (heal) {
            Debug.Log("I come here");
            GameObject healingCir = transform.GetChild(0).gameObject;
            healingCir.SetActive(true);
            ParticleSystem healPS = healingCir.GetComponent<ParticleSystem>();
            healPS.Play();
        }
        if (slow) {
            GameObject slowingCir = transform.GetChild(1).gameObject;
            slowingCir.SetActive(true);
            ParticleSystem slowPS = slowingCir.GetComponent<ParticleSystem>();
            slowPS.Play();
        }
        if (speedUp) {
            GameObject speedingCir = transform.GetChild(2).gameObject;
            speedingCir.SetActive(true);
            ParticleSystem speedPS = speedingCir.GetComponent<ParticleSystem>();
            speedPS.Play();
        }
        if (trapped) {
            GameObject trappingCir = transform.GetChild(3).gameObject;
            trappingCir.SetActive(true);
            ParticleSystem trapPS = trappingCir.GetComponent<ParticleSystem>();
            trapPS.Play();
        }
        if (poison) {
            GameObject poisonCir = transform.GetChild(4).gameObject;
            poisonCir.SetActive(true);
            ParticleSystem poisonPS = poisonCir.GetComponent<ParticleSystem>();
            poisonPS.Play();
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
                    unitRb.velocity = (1 - slowRate) * speedDictionary[unit.gameObject.name];
                }
                if (speedUp) {
                    unitRb.velocity = (1 + speedUpRate) * speedDictionary[unit.gameObject.name];
                }
                if (trapped) {
                    unitRb.velocity = Vector3.zero;
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

    public void resumeSpeed(Collider unit) {
        Rigidbody unitRb = unit.gameObject.GetComponent<Rigidbody>();
        unitRb.velocity = speedDictionary[unit.gameObject.name];
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
        Destroy(gameObject);
    }
}
