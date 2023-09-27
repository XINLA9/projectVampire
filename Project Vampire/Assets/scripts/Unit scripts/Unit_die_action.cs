using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_die_action : MonoBehaviour
{
    private Unit_attributes _attributes;
    private float _HP;
    // Start is called before the first frame update
    void Start()
    {
        _attributes = GetComponent<Unit_attributes>();
        _HP = _attributes.HP;
    }

    // Update is called once per frame
    void Update()
    {
        if (_HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
