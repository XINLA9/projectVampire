using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEnemy : MonoBehaviour
{
    private Attributes _attributes;
    private GameObject _target;
    // Start is called before the first frame update
    void Start()
    {
        _attributes = GetComponent<Attributes>();
    }

    // Update is called once per frame
    void Update()
    {
        //_target = _attributes.moveGoal;
        //_target = FindNearestEnemy();
        //if (_target &&_target.gameObject.GetComponent<Attributes>().isDead)
        //{
        //    _target = null;
        //}
        _attributes.moveGoal = FindNearestEnemy();
        if (_attributes.moveGoal && _attributes.moveGoal.gameObject.GetComponent<Attributes>().isDead)
        {
            _attributes.moveGoal = null;
        }
    }
    private GameObject FindNearestEnemy()
    {
        string goalTag;
        GameObject nearestEnemy = null;
        if (gameObject.tag == "hunter")
        {
            goalTag = "monster";
        }
        else { goalTag = "hunter"; }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(goalTag);

        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance && !enemy.gameObject.GetComponent<Attributes>().isDead)
            //if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }
}
