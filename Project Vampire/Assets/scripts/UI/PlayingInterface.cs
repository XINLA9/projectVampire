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
    private TextMeshProUGUI waveText;
    private GameManager gameManager;
    

    void Start(){
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void show(){
        gameScreen.gameObject.SetActive(true);
    }

    public void updateAllies(){
        allieText[0].text = "Wave " + gameManager.waveNum;
        for(int i = 1; i < gameManager.alliePrefabs.Length + 1; i++)
        {
            allieText[i].text = i + "\\" + gameManager.alliePrefabs[i - 1].name + ":" + gameManager.allieRemain[i - 1];
        }
    }

    public void showLoseScreen(){
        restartButton.gameObject.SetActive(true);
        gameScreen.SetActive(false);
        loseText.gameObject.SetActive(true);
    }

    public void setRed(int i){
        foreach(TextMeshProUGUI text in allieText){
            text.color = new Color(1.0f, 1.0f, 1.0f);
        }
        allieText[i].color = new Color(1.0f, 0.0f, 0.0f);
    }
}
