using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterMapButton : MonoBehaviour
{

    //reference for two TextMeshPro
    public TextMeshProUGUI characterDescription;
    public TextMeshProUGUI mapDescription;

    public GameObject weaponList1;
    public GameObject weaponList2;
    public GameObject weaponList3;

    //when click the character buttons
    public void OnCharacterClicked(int characterIndex){
        switch(characterIndex){
            case 0:
                weaponList1.SetActive(true);
                weaponList2.SetActive(false);
                weaponList3.SetActive(false);
                characterDescription.text = "Now character 1 is mixed units";
                PlayerPrefs.SetInt("characterType", 0);
                break;

            case 1:
                weaponList1.SetActive(false);
                weaponList2.SetActive(true);
                weaponList3.SetActive(false);
                characterDescription.text = "Now character 2 only have ork";
                PlayerPrefs.SetInt("characterType", 1);
                break;

            case 2:
                weaponList1.SetActive(false);
                weaponList2.SetActive(false);
                weaponList3.SetActive(true);
                characterDescription.text = "Now character 3 only have spider";
                PlayerPrefs.SetInt("characterType", 2);
                break;
        }
    }
    //when click the map buttons
    public void OnMapClicked(int mapIndex){
        switch(mapIndex){
            case 0:
            mapDescription.text = "Forest";
            PlayerPrefs.SetInt("mapType", 0);
            break;

            case 1:
            mapDescription.text = "Gravey Yard";
            PlayerPrefs.SetInt("mapType", 1);
            break;

            case 2:
            mapDescription.text = "Castle";
            PlayerPrefs.SetInt("mapType", 2);
            break;
        }
    }

}
