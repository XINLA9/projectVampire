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
    public GameObject winPage;
    public TextMeshProUGUI loseText;
    public GameObject gameScreen;
    public List<TextMeshProUGUI> allieText;
    public List<TextMeshProUGUI> unitTextInfo;
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

        if(gameManager.getAllieNo() != null && gameManager.getAllieNo() >= 0){

            unitTextInfo[0].text = gameManager.getAlliePrefebs()[gameManager.getAllieNo()].name;
            unitTextInfo[1].text = "+HP: " + gameManager.getAlliePrefebs()[gameManager.getAllieNo()].GetComponent<Attributes>().HP_max.ToString();
            unitTextInfo[2].text = "+Attack: " + gameManager.getAlliePrefebs()[gameManager.getAllieNo()].GetComponent<Attributes>().attack_base.ToString();
            unitTextInfo[3].text = "+Defense: " +gameManager.getAlliePrefebs()[gameManager.getAllieNo()].GetComponent<Attributes>().defense_base.ToString();
            unitTextInfo[4].text = "+Speed: " + gameManager.getAlliePrefebs()[gameManager.getAllieNo()].GetComponent<Attributes>().maxSpeed.ToString();
            unitTextInfo[5].text = "+Description: " + gameManager.getAlliePrefebs()[gameManager.getAllieNo()].GetComponent<Attributes>().description.ToString();

        }


    }

    public void showLoseScreen(){
        restartButton.gameObject.SetActive(true);
        gameScreen.SetActive(false);
        loseText.gameObject.SetActive(true);
    }

    public void showWinScreen(){
        //restartButton.gameObject.SetActive(true);
        gameScreen.SetActive(false);
        winPage.SetActive(true);
        //winText.gameObject.SetActive(true);

    }

    public void setRed(int i){
        foreach(TextMeshProUGUI text in allieText){
            text.color = new Color(1.0f, 1.0f, 1.0f);
        }
        allieText[i].color = new Color(1.0f, 0.0f, 0.0f);
    }


    public void onClick_0(){
        gameManager.setAllieNo(0);
        gameManager.playingInterfaces.setRed(1);
        gameManager.unitInfoPanel.SetActive(true);
    }
    public void onClick_1(){
        gameManager.setAllieNo(1);
        gameManager.playingInterfaces.setRed(2);
        gameManager.unitInfoPanel.SetActive(true);
    }
    public void onClick_2(){
        gameManager.setAllieNo(2);
        gameManager.playingInterfaces.setRed(3);
        gameManager.unitInfoPanel.SetActive(true);
    }
    public void onClick_3(){
        gameManager.setAllieNo(3);
        gameManager.playingInterfaces.setRed(4);
        gameManager.unitInfoPanel.SetActive(true);
    }
    public void onClick_4(){
        gameManager.setAllieNo(4);
        gameManager.playingInterfaces.setRed(5);
        gameManager.unitInfoPanel.SetActive(true);
    }
    public void onClick_5(){
        gameManager.setAllieNo(5);
        gameManager.playingInterfaces.setRed(6);
        gameManager.unitInfoPanel.SetActive(true);
    }

}
