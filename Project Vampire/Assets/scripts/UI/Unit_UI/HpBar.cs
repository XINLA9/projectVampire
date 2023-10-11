using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    private GameObject unit;
    private Attributes attributes;
    private Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        unit = gameObject.transform.parent.parent.gameObject;
        attributes = unit.GetComponent<Attributes>();   
        slider = gameObject.GetComponent<Slider>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {   
        // Have the 
        Vector3 screenPos = Camera.main.WorldToScreenPoint(unit.transform.position);
        gameObject.transform.position = screenPos + new Vector3(0, 50, 0);
        //gameObject.transform.localScale = new Vector3(unit.transform.localScale.x, 1, 1);
        slider.value = attributes.HP / attributes.HP_max;
    }
}
