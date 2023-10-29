using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NewWavePanel : MonoBehaviour
{

    //UI components

    public GameObject addNumberObject;
    public GameObject addWeaponObject;
    public GameManager gameManager; //reference to GameManager script
    public GameObject selectionPanel;
    public int[] incrementValues;

    public Text[] allyButtonTexts;
    private Dictionary<string, string[]> characterAllies = new Dictionary<string, string[]>();

    public int characterChosenType;
    public string characterName;

    public TextMeshProUGUI weaponButton1Text1;
    public TextMeshProUGUI weaponButton2Text2; 
    public TextMeshProUGUI weaponButton3Text3;
    public TextMeshProUGUI weaponButton4Text4;
    public TextMeshProUGUI weaponButton5Text5;
    public TextMeshProUGUI weaponButton6Text6;




    void Start()
    {
        incrementValues = new int[] {4, 4, 2, 2, 1, 1};       



        characterChosenType = gameManager.charactorType;

        switch(characterChosenType){
            case 0:

                weaponButton1Text1.text = "Add " +incrementValues[0]+ " more " + gameManager.getAlliePrefebs()[0].name;
                weaponButton2Text2.text = "Add " +incrementValues[1]+ " more " + gameManager.getAlliePrefebs()[1].name;
                weaponButton3Text3.text = "Add " +incrementValues[2]+ " more " + gameManager.getAlliePrefebs()[2].name;
                weaponButton4Text4.text = "Add " +incrementValues[3]+ " more " + gameManager.getAlliePrefebs()[3].name;
                weaponButton5Text5.text = "Add " +incrementValues[4]+ " more " + gameManager.getAlliePrefebs()[4].name;
                weaponButton6Text6.text = "Add " +incrementValues[5]+ " more " + gameManager.getAlliePrefebs()[5].name;
                break;
            case 1:
                weaponButton1Text1.text = "Add " +incrementValues[0]+ " more Ork";
                weaponButton2Text2.text = "Add " +incrementValues[1]+ " more Bear";
                weaponButton3Text3.text = "Add " +incrementValues[2]+ " more Black Window";
                weaponButton4Text4.text = "Add " +incrementValues[3]+ " more Blue Dragon";
                weaponButton5Text5.text = "Add " +incrementValues[4]+ " more unique 2.1";
                weaponButton6Text6.text = "Add " +incrementValues[5]+ " more unique 2.2";
                break;            
            case 2:
                characterName = "Character3";
                weaponButton1Text1.text = "Add " +incrementValues[0]+ " more Ork";
                weaponButton2Text2.text = "Add " +incrementValues[1]+ " more Bear";
                weaponButton3Text3.text = "Add " +incrementValues[2]+ " more Black Window";
                weaponButton4Text4.text = "Add " +incrementValues[3]+ " more Blue Dragon";
                weaponButton5Text5.text = "Add " +incrementValues[4]+ " more unique 3.1";
                weaponButton6Text6.text = "Add " +incrementValues[5]+ " more unique 3.2";
                // UpdateButtonTextsBasedOnCharacter();
                break;           
            default:
                Debug.Log("Error in charactor choosing");
                break;
        }

    }

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
