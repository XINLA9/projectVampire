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



    //when click the character buttons
    public void OnCharacterClicked(int characterIndex){
        switch(characterIndex){
            case 0:

                characterDescription.text = "An ancient and powerful vampire whose origin and name have been lost to time. Centuries ago, he arrived in this land, subdued dragons with his formidable magic, and rebuilt the ruins of the Black Forest castle. He has always maintained a delicate distance from humans, until the greedy king coveted his castle. His forces include a vast array of undead and demonic creatures, making him the most balanced lord, ideal for first-time players.";
                PlayerPrefs.SetInt("characterType", 0);
                break;

            case 1:

                characterDescription.text = "The vast caverns beneath the Black Forest are home to a world of giant insects, ruled by the powerful Beetle King. The surface war has disturbed the subterranean dwellers, prompting the king to lead his warriors to the surface to join the battle. His forces are comprised of numerous insect units that can overwhelm the enemy with their sheer numbers.";
                PlayerPrefs.SetInt("characterType", 1);
                break;

            case 2:

                characterDescription.text = "The Lich Sage was once a learned scholar and magician, executed for his radical views that offended the king but resurrected as a lich. Now he resides in the Earl's castle, continuing his research, until the king's insolent envoy interrupted him. His forces consist of numerous magical creatures, suitable for players with a deep understanding of the game.";
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
