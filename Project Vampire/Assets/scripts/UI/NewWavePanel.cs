using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWavePanel : MonoBehaviour
{

    //UI components
    public GameObject addNumberButton;
    public GameObject addWeaponButton;
    public GameObject addNumberObject;
    public GameObject addWeaponObject;
    public GameManager gameManager; //reference to GameManager script
    public GameObject selectionPanel;
    public int[] incrementValues;

    void Start()
    {
        incrementValues = new int[] {4, 4, 2, 2, 1, 1};        
    }

    public void OnAddNumberButtonClick(){

        //hide the buttons
        addNumberButton.SetActive(false);
        addWeaponButton.SetActive(false);
        //show the target objetcs
        addNumberObject.SetActive(true);

    }

    public void OnAddWeaponButtonClick(){

        //hide the buttons
        addNumberButton.SetActive(false);
        addWeaponButton.SetActive(false);
   
        //show the target objetcs
        addWeaponObject.SetActive(true);

    }
    //Finish updating the number of weapons, start the next wave
    // public void OnStartNewWaveAfterSelectionClick(){
    // selectionPanel.SetActive(false);
    // gameManager.NewWave();
    // }

    public void IncreaseAllyCountButton0(){
        Debug.Log("Button 1 clicked");
        IncreaseAllyCount(0);
    }

    public void IncreaseAllyCountButton1(){
        Debug.Log("Button 2 clicked");
        IncreaseAllyCount(1);
    }

    public void IncreaseAllyCountButton2(){
        Debug.Log("Button 3 clicked");
        IncreaseAllyCount(2);
    }

    public void IncreaseAllyCountButton3(){
        Debug.Log("Button 4 clicked");
        IncreaseAllyCount(3);
    }

    public void IncreaseAllyCountButton4(){
        Debug.Log("Button 5 clicked");
        IncreaseAllyCount(4);
    }

    public void IncreaseAllyCountButton5(){
        Debug.Log("Button 6 clicked");
        IncreaseAllyCount(5);
    }
    
    public void IncreaseAllyCount(int allyIndex){
        Debug.Log("IncreaseAllyCount method is called");
        // Then, let the GameManager handle the game logic
        selectionPanel.SetActive(false);
        gameManager.addAllieMaxNum(allyIndex, incrementValues[allyIndex]);
        gameManager.NewWave();
    }
}
