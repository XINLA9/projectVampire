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
                characterDescription.text = "Character 1 is avaliable now";
                PlayerPrefs.SetInt("characterNum", 1);
                break;

            case 1:
                // weaponList1.SetActive(false);
                weaponList2.SetActive(true);
                // weaponList3.SetActive(false);
                characterDescription.text = "Character 2 is unavaliable now";
                PlayerPrefs.SetInt("characterNum", 2);
                break;

            case 2:

                // weaponList1.SetActive(false);
                // weaponList2.SetActive(false);
                weaponList3.SetActive(true);
                characterDescription.text = "Character 3 is unavaliable now";
                PlayerPrefs.SetInt("characterNum", 3);
                break;
        }
    }
    //when click the map  buttons
    public void OnMapClicked(int mapIndex){
        switch(mapIndex){
            case 0:
            mapDescription.text = "Map 1 is avaliable now";
            PlayerPrefs.SetInt("mapNum", 1);
            break;

            case 1:
            mapDescription.text = "Map 2 is unavaliable now";
            PlayerPrefs.SetInt("mapNum", 2);
            break;

            case 2:
            mapDescription.text = "Map 3 is unavaliable now";
            PlayerPrefs.SetInt("mapNum", 3);
            break;
        }
    }

}
