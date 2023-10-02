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
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //when click the character buttons
    public void OnCharacterClicked(int characterIndex){
        switch(characterIndex){
            case 0:
            characterDescription.text = "Character 1 is ..";
            break;

            case 1:
            characterDescription.text = "Character 2 is ..";
            break;

            case 2:
            characterDescription.text = "Character 3 is ..";
            break;
        }
    }
    //when click the map  buttons
    public void OnMapClicked(int mapIndex){
        switch(mapIndex){
            case 0:
            mapDescription.text = "Map 1 is ..";
            break;

            case 1:
            mapDescription.text = "Map 2 is ..";
            break;

            case 2:
            mapDescription.text = "Map 3 is ..";
            break;
        }
    }

}
