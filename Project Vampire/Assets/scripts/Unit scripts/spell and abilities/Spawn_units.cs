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
    void Start()
    {
        _attributes = GetComponent<Attributes>();
    }

    void Update()
    {

    }
    private void OnMouseDown()
    {
        Debug.Log("spawn");
        if (_attributes.mana - manaCost >= 0)
        {
            Vector3 spawnPosition = transform.position;
            for (int i = 0; i < spawn_number; i++)
            {
                Instantiate(spawn_unit, spawnPosition, Quaternion.identity);
            }
            _attributes.mana -= manaCost;
            
        }
    }
}
