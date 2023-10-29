using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHoverUI : MonoBehaviour
{
    public GameObject uiElement;
    public bool isRangePower;
    public bool isRange;
    public bool isSkeleton;

    private void Start()
    {
        if (isRangePower) {
            uiElement = gameObject.transform.GetChild(5).gameObject;
            uiElement.SetActive(true);
        } else if (isRange) {
            uiElement = gameObject.transform.GetChild(4).gameObject;
            uiElement.SetActive(true);
        } else if (isSkeleton) {
            uiElement = gameObject.transform.GetChild(2).gameObject;
            uiElement.SetActive(true);
        } else {
            uiElement = gameObject.transform.GetChild(0).gameObject;
            uiElement.SetActive(true);
        }
    }

    //private void OnMouseEnter()
    //{
    //    uiElement.SetActive(true);
    //}

    //private void OnMouseExit()
    //{
    //    uiElement.SetActive(false);
    //}

    
}
