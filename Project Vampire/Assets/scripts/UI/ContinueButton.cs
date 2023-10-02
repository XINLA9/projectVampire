using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this class for transfer to another panel
public class ContinueButton : MonoBehaviour
{
    public GameObject currentPanel;
    public GameObject targetPanel;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnContinueButtonClick(){

        //hide current panel;
        currentPanel.SetActive(false);
      

        //show the target panel
        targetPanel.SetActive(true);

    }
    
}

