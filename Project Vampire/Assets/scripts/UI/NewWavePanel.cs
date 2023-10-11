using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWavePanel : MonoBehaviour
{
    public GameObject addNumberButton;
    public GameObject addWeaponButton;

    public GameObject addNumberObject;
    public GameObject addWeaponObject;

    public GameObject continueButton;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAddNumberButtonClick(){

        //hide the buttons
        addNumberButton.SetActive(false);
        addWeaponButton.SetActive(false);
   
        //show the target objetcs
        continueButton.SetActive(true);
        addNumberObject.SetActive(true);

    }

    public void OnAddWeaponButtonClick(){

        //hide the buttons
        addNumberButton.SetActive(false);
        addWeaponButton.SetActive(false);
   
        //show the target objetcs
        continueButton.SetActive(true);
        addWeaponObject.SetActive(true);

    }
}
