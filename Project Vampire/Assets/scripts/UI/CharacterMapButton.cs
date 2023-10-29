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
            mapDescription.text = "Spanning nearly two-thirds of the kingdom's land, the vast Black Forest is home to a myriad of dangerous and terrifying creatures. From perilous black bears and mighty dragons to bizarre spiders, the forest is a haven for the fearsome. Yet, many choose to dwell on the edges of the Black Forest, for they believe that compared to the dragons, spiders, or the enigmatic Earl, the greedy king and his overbearing soldiers pose a much greater and merciless threat.";
            PlayerPrefs.SetInt("mapType", 0);
            break;

            case 1:
            mapDescription.text = "Many liches prefer the solitude of graveyards, where they can conduct their research undisturbed by the pesky Church or nobility and have an unending supply of experimental materials at hand. For the Lich Sage, the graveyard situated on the border of the Black Forest and the kingdom represents such an ideal haven.";
            PlayerPrefs.SetInt("mapType", 1);
            break;

            case 2:
            mapDescription.text = "It is said that deep within the Black Forest stood the kingdom's capital a millennium ago, until dragons descended, laying waste to everything and claiming the royal castle's ruins as their lair. Upon the Earl's arrival, he wielded his magic to reconstruct the castle. Now, the castle's scale and grandeur rival its former glory, undiminished by time.";
            PlayerPrefs.SetInt("mapType", 2);
            break;
        }
    }

}
