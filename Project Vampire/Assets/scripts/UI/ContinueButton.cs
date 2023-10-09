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
    public GameObject background;
    private PlayingInterfaces playingInterfaces;

    void Start(){
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playingInterfaces = FindObjectOfType<PlayingInterfaces>();
    }

    public void OnContinueButtonClick(){
        characterNum = PlayerPrefs.GetInt("characterNum");
        mapNum = PlayerPrefs.GetInt("mapNum");
        currentPanel.SetActive(false);
        background.SetActive(false);
        playingInterfaces.show();
        gameManager.StartGame();
    }
}

