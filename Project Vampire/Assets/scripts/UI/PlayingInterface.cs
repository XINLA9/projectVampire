using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using System.Linq;

public class PlayingInterfaces : MonoBehaviour
{
    // attribute for UI 
    public Button restartButton;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;
    public GameObject gameScreen;
    public List<TextMeshProUGUI> allieText;
    public List<GameObject> allieProfolio;
    private GameManager gameManager;
    

    void Start(){
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void show(){
        gameScreen.gameObject.SetActive(true);
    }

    public void updateAllies(){
        allieText[0].text = "Wave " + gameManager.getWaveNum();
        for(int i = 1; i < gameManager.getAlliePrefebs().Length + 1; i++)
        {
            allieText[i].text = i + "\\" + gameManager.getAlliePrefebs()[i - 1].name + ":" + gameManager.getAllieRemains()[i - 1];
            allieProfolio[i - 1].GetComponent<Image>().sprite = gameManager.getAllieProfolios()[i-1];

        }
        allieText[7].text = "Enemy Remain: " + gameManager.getEnemyRemain().ToString();
    }

    public void showLoseScreen(){
        restartButton.gameObject.SetActive(true);
        gameScreen.SetActive(false);
        loseText.gameObject.SetActive(true);
    }

    public void showWinScreen(){
        restartButton.gameObject.SetActive(true);
        gameScreen.SetActive(false);
        winText.gameObject.SetActive(true);
    }

    public void setRed(int i){
        foreach(TextMeshProUGUI text in allieText){
            text.color = new Color(1.0f, 1.0f, 1.0f);
        }
        allieText[i].color = new Color(1.0f, 0.0f, 0.0f);
    }

    public void setAllyUnitInfoActive(int i){
        switch(i){

            case 1:
                gameManager.OrkImage.SetActive(true);
                gameManager.BlackWindownSideImage.SetActive(false);
                break;

            case 2:
                gameManager.BlackWindownSideImage.SetActive(true);
                gameManager.OrkImage.SetActive(false);
                break;
            default:
                Debug.Log("Error in charactor choosing");
                break;
        }
    }

    public void onClick_0(){
        gameManager.setAllieNo(0);
        gameManager.playingInterfaces.setRed(1);
        gameManager.playingInterfaces.setAllyUnitInfoActive(1);
    }
    public void onClick_1(){
        gameManager.setAllieNo(1);
        gameManager.playingInterfaces.setRed(2);
        gameManager.playingInterfaces.setAllyUnitInfoActive(2);
    }
    public void onClick_2(){
        gameManager.setAllieNo(2);
        gameManager.playingInterfaces.setRed(3);
        gameManager.playingInterfaces.setAllyUnitInfoActive(3);
    }
    public void onClick_3(){
        gameManager.setAllieNo(3);
        gameManager.playingInterfaces.setRed(4);
        gameManager.playingInterfaces.setAllyUnitInfoActive(4);
    }
    public void onClick_4(){
        gameManager.setAllieNo(4);
        gameManager.playingInterfaces.setRed(5);
        gameManager.playingInterfaces.setAllyUnitInfoActive(5);
    }
    public void onClick_5(){
        gameManager.setAllieNo(5);
        gameManager.playingInterfaces.setRed(6);
        gameManager.playingInterfaces.setAllyUnitInfoActive(6);
    }

}
