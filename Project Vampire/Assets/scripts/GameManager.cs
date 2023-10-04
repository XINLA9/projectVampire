using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public GameObject[] hunterPrefabs_forest;
    // public GameObject[] hunterPrefabs_graveyYard;
    // public GameObject[] hunterPrefabs_castle;
    public GameObject[] monsterPrefabs;
    private GameObject[] activeHunterPrefabs = {};
    public GameObject ForestMap;
    // public GameObject GraveyYardMap;
    public GameObject CastleMap;
    private float spawnRangeX = 12;
    private float spawnRangeZ = 6;
    private float outerBoundaryX = 43f;
    private float outerBoundaryZ = 24f;
    private float innerBoundaryX = 37f;
    private float innerBoundaryZ = 16f;
    private int monsterNo = 0;
    private bool isGameActive = false;
    private int hunterVar;
    private int monsterVar;
    private int[] enemyRatio = new int[10];
    private int waveNum = 1;
    private int[] monsterRatio = new int[10];
    private int[] monsterRemain = new int[10];

    // attribute for UI 
    public Button restartButton;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;
    public GameObject gameScreen;
    public TextMeshProUGUI numText1;
    public TextMeshProUGUI numText2;
    public TextMeshProUGUI numText3;
    public TextMeshProUGUI waveText;


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < monsterVar; i++){
            enemyRatio[i] = 4 / (i + 1) + 1;//will be modified later
            monsterRatio[i] = 6 / (2 * i + 1) + 1;//will be modified later
        }
    }
    // begin the game
    public void StartGame()
    {
        int charactorNum = PlayerPrefs.GetInt("characterNum");
        int mapNum = PlayerPrefs.GetInt("mapNum");
        switch(charactorNum){
            case 1:
                activeHunterPrefabs = hunterPrefabs_forest;
                break;
            default:
                Debug.Log("Will be developed later");
                break;
        }
        switch(mapNum){
            case 1:
                ForestMap.SetActive(true);
                break;
            case 2:
                CastleMap.SetActive(true);
                break;            
            default:
                Debug.Log("Will be developed later");
                break;
        }
        monsterVar = monsterPrefabs.Length;
        hunterVar = activeHunterPrefabs.Length;
        for(int i = 0; i < monsterVar; i++){
            enemyRatio[i] = 4 / (i + 1) + 1;//will be modified later
            monsterRatio[i] = 6 / (2 * i + 1) + 1;//will be modified later
        }
        for(int i = 0; i < monsterRemain.Length; i++){
            monsterRemain[i] = monsterRatio[i] * waveNum;
        }
        Debug.Log(monsterRemain);
        isGameActive = true;
        gameScreen.gameObject.SetActive(true);
        SpawnEnemy();
    }
    // Update is called once per frame
    void Update()
    {
        // spawn monster when player click on the map
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.up * 2f);
            float rayDistance;
            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 clickPosition = ray.GetPoint(rayDistance);
                if (IsInAllowedRange(clickPosition) && isGameActive)
                {
                    SpawnMonsters(monsterNo, clickPosition);
                }
            }
        }
        if(isGameActive){
            WaveCheck();
            numText1.text = "wolf:" + monsterRemain[0];
            numText2.text = "horse:" + monsterRemain[1];
            numText3.text = "mist:" + monsterRemain[2];
            waveText.text = "wave:" + waveNum;
        }
    }
    Vector3 GenerateSpawnPosition()
    {
        float xPos = UnityEngine.Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = UnityEngine.Random.Range(spawnRangeZ, -spawnRangeZ);
        return new Vector3(xPos, 1, zPos);
    }
    // generate the hunters for this game
    void SpawnEnemy()
    {
        for (int i = 0; i < hunterVar; i++){
            for (int j = 0; j < waveNum * enemyRatio[i]; j++){
                Instantiate(activeHunterPrefabs[i], GenerateSpawnPosition(), activeHunterPrefabs[i].transform.rotation);
            }
        }
    }
    public void SetMonster(int monsterNo)
    {
        this.monsterNo = monsterNo;
    }

    bool IsInAllowedRange(Vector3 position)
    {
        if (position.x < -outerBoundaryX || position.x > outerBoundaryX){
            Debug.Log("Can't place outside the map" + position);
            return false;
        }
        if (position.z < -outerBoundaryZ || position.z > outerBoundaryZ){
            Debug.Log("Can't place outside the map" + position); 
            return false;
        }
        if (position.z < innerBoundaryZ && position.z > -innerBoundaryZ && position.x < innerBoundaryX && position.x > -innerBoundaryX){
            Debug.Log("Can't place inside the forest, its too close to the hunter!" + position);
            return false;
        }
        return true;
    }

    void SpawnMonsters(int monsterNO, Vector3 position)
    {
        if (monsterRemain[monsterNO] > 0)
        {
            Instantiate(monsterPrefabs[monsterNO], position, monsterPrefabs[monsterNO].transform.rotation);
            monsterRemain[monsterNO]--;
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void WaveCheck()
    {
        int hunterNumber = GameObject.FindGameObjectsWithTag("hunter").Length;
        int monsterNumber = GameObject.FindGameObjectsWithTag("monster").Length;
        int totalMonsterRemaining = 0;
        foreach (int remaining in monsterRemain)
        {
            totalMonsterRemaining += remaining;
        }
        if (hunterNumber == 0)
        {
            NewWave();
        }
        if(totalMonsterRemaining == 0 && monsterNumber == 0)
        {
            restartButton.gameObject.SetActive(true);
            gameScreen.SetActive(false);
            loseText.gameObject.SetActive(true);
        }            
    }
    public void NewWave(){
        waveNum++;
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("monster");

        foreach (GameObject monster in monsters)
        {
            Destroy(monster);
        }
        SpawnEnemy();
        for(int i = 0; i < monsterRemain.Length; i++){
            monsterRemain[i] = monsterRatio[i] * waveNum;
        }
        isGameActive = true;
    }
}
