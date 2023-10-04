using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this class for transfer to another panel
public class ContinueButton : MonoBehaviour
{
    public GameObject currentPanel;
    private GameManager gameManager;
    private int mapNum;
    private int characterNum;

    void Start(){
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void OnContinueButtonClick(){
        characterNum = PlayerPrefs.GetInt("characterNum");
        mapNum = PlayerPrefs.GetInt("mapNum");
        currentPanel.SetActive(false);
        gameManager.StartGame();
        // if (mapNum == 1 && characterNum == 1){
        //     currentPanel.SetActive(false);
        //     gameManager.StartGame();
        // }
        // else{
        //     Debug.Log("Current Map or character not avaliable now, please choose map 1 and character 1");
        // }
    }
}

