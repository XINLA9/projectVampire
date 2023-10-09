using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class WeaponInfo : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    //references for all components;
    public GameObject weaponPanel;
    public string weaponDescription;
    public TextMeshProUGUI weaponInfoText;

    //public Vector2 offset = new Vector2(0,10);



    // Start is called before the first frame update
    void Start()
    {
        //weaponInfoText = weaponPanel.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    //when the mouse over the button
    public void OnPointerEnter(PointerEventData PointerEventData){

        //set the panel position
        Vector2 newPosition = (Vector2) Input.mousePosition;
        weaponPanel.transform.position = newPosition;

        weaponInfoText.text = weaponDescription;
        weaponPanel.SetActive(true);//show it
        Debug.Log("enter");
    }
    
    //when mouse exit the button
    public void OnPointerExit(PointerEventData eventData){
            
        weaponPanel.SetActive(false);//hide it
        Debug.Log("exit");

    }

}
