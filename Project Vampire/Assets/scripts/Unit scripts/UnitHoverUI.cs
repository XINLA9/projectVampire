using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHoverUI : MonoBehaviour
{
    public GameObject uiElement;

    private void Start()
    {
        uiElement = gameObject.transform.GetChild(0).gameObject;
        uiElement.SetActive(true);
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
