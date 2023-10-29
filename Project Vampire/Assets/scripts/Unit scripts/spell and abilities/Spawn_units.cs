using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawn_units : MonoBehaviour
{
    // Start is called before the first frame update
    private Attributes _attributes;
    private Animator _animator;
    public GameObject spawn_unit;
    public int spawn_number;
    public float manaCost;
    public float spawnInterval; 

    private float nextSpawnTime; 

    void Start()
    {
        _attributes = GetComponent<Attributes>();
        nextSpawnTime = Time.time + spawnInterval; 
    }

    void Update()
    {
        
        if (Time.time >= nextSpawnTime && _attributes.mana - manaCost >= 0)
        {
            if(!_attributes.isDead)
            {
                SpawnUnits(); 
            }
            nextSpawnTime = Time.time + spawnInterval; 
        }
    }

    private void SpawnUnits()
    {
        Vector3 spawnPosition = transform.position;
        for (int i = 0; i < spawn_number; i++)
        {
            GameObject spawnedUnit = Instantiate(spawn_unit, spawnPosition, Quaternion.identity);
            spawnedUnit.tag = gameObject.tag;
        }
        _attributes.mana -= manaCost;
        Debug.Log("Spawned Units");
    }

}
