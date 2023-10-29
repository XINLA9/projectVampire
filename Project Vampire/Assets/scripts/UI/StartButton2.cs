using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton2 : MonoBehaviour
{
    //references for all buttons and texts
    public GameObject titleText;
    public GameObject startButton;
    public GameObject settingButton;
    public GameObject targetPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnStartButtonClick(){

        //hide the buttons
        titleText.SetActive(false);
        startButton.SetActive(false);
        settingButton.SetActive(false);

        //show the target panel
        targetPanel.SetActive(true);

    }
}
