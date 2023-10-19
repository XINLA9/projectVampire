using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialsButtons : MonoBehaviour
{
    public GameObject Tutorial1Image;
    public GameObject Tutorial2Image;
    public GameObject Tutorial3Image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTutorialButtonClick(){
  
    Tutorial1Image.SetActive(true);

    }

    public void OnTutorial1NextButtonClick(){
  
    Tutorial1Image.SetActive(false);
    Tutorial2Image.SetActive(true);

    }

    public void OnTutorial2BackButtonClick(){
  
    
    Tutorial2Image.SetActive(false);
    Tutorial1Image.SetActive(true);

    }

    public void OnTutorial2NextButtonClick(){
  
    Tutorial2Image.SetActive(false);
    Tutorial3Image.SetActive(true);
    
    }

    public void OnTutorial3BackButtonClick(){
  
    Tutorial3Image.SetActive(false);
    Tutorial2Image.SetActive(true);
    
    }
    public void OnTutorial3CloseButtonClick(){
  
    Tutorial3Image.SetActive(false);
    
    }
}
