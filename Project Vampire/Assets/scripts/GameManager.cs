using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs_forest;
    // public GameObject[] hunterPrefabs_graveyYard;
    // public GameObject[] hunterPrefabs_castle;
    public GameObject[] alliePrefabs;
    private GameObject[] activeEnemyPrefabs = {};
    public GameObject ForestMap;
    // public GameObject GraveyYardMap;
    public GameObject CastleMap;
    private float spawnRangeX = 12;
    private float spawnRangeZ = 6;
    private float outerBoundaryX = 43f;
    private float outerBoundaryZ = 24f;
    private float innerBoundaryX = 37f;
    private float innerBoundaryZ = 16f;
    private int allieNo = -1;
    private bool isGameActive = false;
    private int enemyVar;
    private int allieVar;
    private int[] enemyRatio = new int[10];
    private int[] allieRatio = new int[10];
    public int[] allieRemain = new int[10];
    public int waveNum = 1;
    public PlayingInterfaces playingInterfaces;
    public GameObject cycle;
    public Material cycleMaterial;

    public GameObject selectionPanel;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < allieVar; i++){
            enemyRatio[i] = 4 / (i + 1) + 1;//will be modified later
            allieRatio[i] = 6 / (2 * i + 1) + 1;//will be modified later
        }
        playingInterfaces = FindObjectOfType<PlayingInterfaces>();
    }
    // begin the game
    public void StartGame()
    {
        int charactorNum = PlayerPrefs.GetInt("characterNum");
        int mapNum = PlayerPrefs.GetInt("mapNum");
        switch(charactorNum){
            case 1:
                activeEnemyPrefabs = enemyPrefabs_forest;
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
        allieVar = alliePrefabs.Length;
        enemyVar = activeEnemyPrefabs.Length;
        for(int i = 0; i < allieVar; i++){
            enemyRatio[i] = 4 / (i + 1) + 1;//will be modified later
            allieRatio[i] = 6 / (2 * i + 1) + 1;//will be modified later
        }
        for(int i = 0; i < allieRemain.Length; i++){
            allieRemain[i] = allieRatio[i] * waveNum;
        }
        Debug.Log(allieRemain);
        isGameActive = true;
        cycle.SetActive(true);
        SpawnEnemy();
        playingInterfaces.updateAllies();
    }
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.up * 2f);
        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 clickPosition = ray.GetPoint(rayDistance);
            cycle.transform.position = clickPosition;
            if(IsInAllowedRange(clickPosition)){
                cycleMaterial.color = Color.green;
            }
            else{
                cycleMaterial.color = Color.red;
            }
            if (Input.GetMouseButtonDown(0))
            {
                if(allieNo < 0){
                    Debug.Log("Press number button to choose a unit before placing it!");
                }
                else{
                    if (IsInAllowedRange(clickPosition) && isGameActive)
                    {
                        SpawnAllies(allieNo, clickPosition);
                    }
                }

            }
        }
        
        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                allieNo = i - 1;
                playingInterfaces.setRed(i);
            }
        }
        if(isGameActive){
            WaveCheck();
            playingInterfaces.updateAllies();
        }
    }
    Vector3 GenerateSpawnPosition()
    {
        float xPos = UnityEngine.Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = UnityEngine.Random.Range(spawnRangeZ, -spawnRangeZ);
        return new Vector3(xPos, 1, zPos);
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < enemyVar; i++){
            for (int j = 0; j < waveNum * enemyRatio[i]; j++){
                GameObject Enemy = Instantiate(activeEnemyPrefabs[i], GenerateSpawnPosition(), activeEnemyPrefabs[i].transform.rotation);
                Enemy.tag = "hunter";
            }
        }
    }
    public void SetAllieNo(int allieNo)
    {
        this.allieNo = allieNo;
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
            Debug.Log("Can't place inside the forest, its too close to the enemy!" + position);
            return false;
        }
        return true;
    }

    void SpawnAllies(int allieNO, Vector3 position)
    {
        if (allieRemain[allieNO] > 0)
        {
            GameObject Allies =  Instantiate(alliePrefabs[allieNO], position, alliePrefabs[allieNO].transform.rotation);
            Allies.tag = "monster";
            allieRemain[allieNO]--;
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void WaveCheck()
    {
        Attributes[] enemyAttributes = GameObject.FindGameObjectsWithTag("hunter")
        .Select(go => go.GetComponent<Attributes>())
        .Where(attributes => attributes != null && attributes.isDead == false)
        .ToArray();

        int numberOfLivingEnemies = enemyAttributes.Length;

        Attributes[] allieAttributes = GameObject.FindGameObjectsWithTag("monster")
        .Select(go => go.GetComponent<Attributes>())
        .Where(attributes => attributes != null && attributes.isDead == false)
        .ToArray();

        int numberOfLivingAllies = allieAttributes.Length;

        int totalAlliesRemaining = 0;
        foreach (int remaining in allieRemain)
        {
            totalAlliesRemaining += remaining;
        }
        if (numberOfLivingEnemies == 0)
        {
            selectionPanel.SetActive(true);
            //NewWave();
        }
        if(totalAlliesRemaining == 0 && numberOfLivingAllies == 0)
        {
            playingInterfaces.showLoseScreen();
        }            
    }
    public void NewWave(int allyIndex, int incrementValue){
        waveNum++;

        // check the index is valid
        if(allyIndex >= 0 && allyIndex < allieRemain.Length)
        {
            allieRemain[allyIndex] += incrementValue; // increse according number
            Debug.Log("Increasing successfully!!");
        }
        else
        {
            Debug.LogWarning("Invalid weapon index!");
        }
        
        //Destroy existing allies
        GameObject[] allies = GameObject.FindGameObjectsWithTag("monster");

        foreach (GameObject allie in allies)
        {
            Destroy(allie);
        }

        SpawnEnemy();



        isGameActive = true;
        playingInterfaces.updateAllies();
    }

    
}
