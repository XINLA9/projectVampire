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
    public GameObject HowToPlayImage;// Instruction Image
    public GameObject BlackWindownSideImage;
    public GameObject OrkImage;
    public GameObject ForestMap;//Map_1
    public GameObject GraveyYardMap;//Map_2
    public GameObject CastleMap;//Map_3
    public PlayingInterfaces playingInterfaces;//UI controller when playing
    public GameObject selectionPanel;//UI between waves
    public GameObject mousePointer;//A cycle that follow the mouse
    public Material cycleMaterial;//The material for the cycle
    public GameObject[] forestEnemyPrefabs;//enemy prefabs for forestMap
    public GameObject[] graveyEnemyPrefabs;//enemy prefabs for graveyMap
    public GameObject[] castleEnemyPrefabs;//enemy prefabs for castleMap
    public GameObject[] alliePrefabs_C1;//allie prefabs for charactor1
    public GameObject[] alliePrefabs_C2;//allie prefabs for charactor2
    public GameObject[] alliePrefabs_C3;//allie prefabs for charactor3

    private GameObject[] activeAlliePrefabs = {};//Allie units that been chosen in a game
    private GameObject[] activeEnemyPrefabs = {};//Enemy units that been chosen in a map
    private int enemyVar = 4;//The number of type for enemy
    private int allieVar = 6;//The number of type for allie
    private int[] allieRemain = new int[6];//The number of remaining allies
    private int[] allieMaxNum = {6, 6, 3, 3, 1, 1};//The number of allies at the beginning of the wave
    private int enemyRemain;//The number of remaining enemys
    private int allieNo = -1;//units that been currently chosen
    private bool isGameActive = false;//Game active flag
    public int waveNum = 0;//current wave number
    public int charactorType = -1;//Charactor chosen
    private int mapType = -1;//Map chosen

    //Boundarys for maps
    private float[] spawnRangeX = {12.0f, 24.0f, 20.0f};
    private float[] spawnRangeZ = {6.0f, 8.0f, 10.0f};
    private float[] outerBoundaryX = {43.0f, 36.0f, 48.0f};
    private float[] outerBoundaryZ = {24.0f, 23.0f, 32.0f};
    private float[] innerBoundaryX = {37.0f, 31.0f, 22.0f};
    private float[] innerBoundaryZ = {16.0f, 11.0f, 25.0f};

    //Enemy spawn in each wave, 5 waves and 4 types of enemy for each map.
    private int[][] activeEnemyWave = new int[5][];
    private int[][] enemyInForestWave = new int[5][]{
        new int[] { 4, 2, 0, 0},
        new int[] { 6, 3, 1, 0},
        new int[] { 6, 4, 2, 1},
        new int[] { 8, 6, 3, 1},
        new int[] { 8, 8, 4, 2}
    };
    private int[][] enemyInGraveyWave = new int[5][]{
        new int[] { 4, 2, 0, 0},
        new int[] { 6, 3, 1, 0},
        new int[] { 6, 4, 2, 1},
        new int[] { 8, 6, 3, 1},
        new int[] { 8, 8, 4, 2}
    };
    private int[][] enemyInCastleWave = new int[5][]{
        new int[] { 4, 2, 0, 0},
        new int[] { 6, 3, 1, 0},
        new int[] { 6, 4, 2, 1},
        new int[] { 8, 6, 3, 1},
        new int[] { 8, 8, 4, 2}
    };

    // Start is called before the first frame update
    void Start()
    {
        playingInterfaces = FindObjectOfType<PlayingInterfaces>();
    }

    // begin the game
    public void StartGame()
    {   
        //initialize map and unit sets
        charactorType = PlayerPrefs.GetInt("characterType");
        mapType = PlayerPrefs.GetInt("mapType");
        switch(mapType){
            case 0:
                ForestMap.SetActive(true);
                HowToPlayImage.SetActive(true);
                activeEnemyPrefabs = forestEnemyPrefabs;
                activeEnemyWave = enemyInForestWave;
                break;
            case 1:
                GraveyYardMap.SetActive(true);
                activeEnemyPrefabs = graveyEnemyPrefabs;
                activeEnemyWave = enemyInGraveyWave;                
                break;
            case 2:
                CastleMap.SetActive(true);
                activeEnemyPrefabs = castleEnemyPrefabs;
                activeEnemyWave = enemyInCastleWave;                
                break;
            default:
                Debug.Log("Error in map choosing");
                break;
        }
        switch(charactorType){
            case 0:
                activeAlliePrefabs = alliePrefabs_C1;
                break;
            case 1:
                activeAlliePrefabs = alliePrefabs_C2;
                break;            
            case 2:
                activeAlliePrefabs = alliePrefabs_C3;
                break;           
            default:
                Debug.Log("Error in charactor choosing");
                break;
        }
        allieVar = activeAlliePrefabs.Length;
        enemyVar = activeEnemyPrefabs.Length;
        mousePointer.SetActive(true);
        NewWave();
    }
    void Update()
    {
        //Find mouse position
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.up * 2f);
        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 clickPosition = ray.GetPoint(rayDistance);
            mousePointer.transform.position = clickPosition;

            // Debug.Log(clickPosition);

            //If the position can be placed, show green, othervise show red
            if(IsInAllowedRange(clickPosition)){
                cycleMaterial.color = Color.green;
            }
            else{
                cycleMaterial.color = Color.red;
            }
            //Spawn a allie unit
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
        //Choose the type of the allie unit to be placed
        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                allieNo = i - 1;
                playingInterfaces.setRed(i);
                playingInterfaces.setAllyUnitInfoActive(i);
            }
        }
        //Check if the wave ends and update the UI
        if(isGameActive){
            WaveCheck();
            playingInterfaces.updateAllies();
        }
    }
    //Generate a Position for spwan enemy
    Vector3 GenerateSpawnPosition()
    {
        if(mapType != -1){
            float xPos = UnityEngine.Random.Range(-spawnRangeX[mapType], spawnRangeX[mapType]);
            float zPos = UnityEngine.Random.Range(spawnRangeZ[mapType], -spawnRangeZ[mapType]);
            return new Vector3(xPos, 1, zPos);
        }
        else{
            return new Vector3(0, 1, 0);
        }
    }
    //Spwan enemy
    void SpawnEnemy()
    {
        for (int i = 0; i < enemyVar; i++){
            for (int j = 0; j < activeEnemyWave[waveNum - 1][i]; j++){
                Debug.Log("Enemy" + j + ": " + activeEnemyWave[waveNum - 1][i]);
                GameObject Enemy = Instantiate(activeEnemyPrefabs[i], GenerateSpawnPosition(), activeEnemyPrefabs[i].transform.rotation);
                Enemy.tag = "hunter";
            }
        }
    }
    //Range check for spawn allies
    bool IsInAllowedRange(Vector3 position)
    {
        if(mapType != -1){
            if (position.x < -outerBoundaryX[mapType] || position.x > outerBoundaryX[mapType]){
                return false;
            }
            if (position.z < -outerBoundaryZ[mapType] || position.z > outerBoundaryZ[mapType]){
                return false;
            }
            if (position.z < innerBoundaryZ[mapType] && position.z > -innerBoundaryZ[mapType] && position.x < innerBoundaryX[mapType] && position.x > -innerBoundaryX[mapType]){
                return false;
            }
            return true;
        }
        else return false;
    }
    //Spawn allies
    void SpawnAllies(int allieNO, Vector3 position)
    {
        if (allieRemain[allieNO] > 0)
        {
            GameObject Allies =  Instantiate(activeAlliePrefabs[allieNO], position, activeAlliePrefabs[allieNO].transform.rotation);
            Allies.tag = "monster";
            allieRemain[allieNO]--;
        }
    }
    //Check if the wave ends
    public void WaveCheck()
    {
        Attributes[] enemyAttributes = GameObject.FindGameObjectsWithTag("hunter")
        .Select(go => go.GetComponent<Attributes>())
        .Where(attributes => attributes != null && attributes.isDead == false)
        .ToArray();

        int numberOfLivingEnemies = enemyAttributes.Length;//Check the number of remaining enemys on the board
        enemyRemain = numberOfLivingEnemies;

        Attributes[] allieAttributes = GameObject.FindGameObjectsWithTag("monster")
        .Select(go => go.GetComponent<Attributes>())
        .Where(attributes => attributes != null && attributes.isDead == false)
        .ToArray();

        int numberOfLivingAllies = allieAttributes.Length;//Check the number of remaining allies on the board

        int totalAlliesRemaining = 0;//Count how many allies are not been placed yet
        foreach (int remaining in allieRemain)
        {
            totalAlliesRemaining += remaining;
        }
        //No allies remaining, game lose
        if(totalAlliesRemaining == 0 && numberOfLivingAllies == 0)
        {
            playingInterfaces.showLoseScreen();
            isGameActive = false;
        }
        //No enemys remaining, next wave             
        if (numberOfLivingEnemies == 0)
        {
            isGameActive = false;

            if(waveNum <= 4){
                selectionPanel.SetActive(true);
                BlackWindownSideImage.SetActive(false);
                OrkImage.SetActive(false);
            }
            else{
                playingInterfaces.showWinScreen();
            }
        }
    }
    //Begin a new wave
    public void NewWave(){
        waveNum++;
        //Destroy existing allies
        GameObject[] allies = GameObject.FindGameObjectsWithTag("monster");
        GameObject[] bodys = GameObject.FindGameObjectsWithTag("Dead");
        foreach (GameObject allie in allies)
        {
            Destroy(allie);
        }
        foreach (GameObject body in bodys)
        {
            Destroy(body);
        }
        SpawnEnemy();
        allieMaxNum.CopyTo(allieRemain, 0);
        isGameActive = true;
        playingInterfaces.updateAllies();
    }
    //Add allie max number
    public void addAllieMaxNum(int allieNo, int num){
        allieMaxNum[allieNo] += num;
    }
    //Restart Game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //Getter functions
    public int getWaveNum(){
        return waveNum;
    }
    public GameObject[] getAlliePrefebs(){
        return activeAlliePrefabs;
    }
    public int[] getAllieRemains(){
        return allieRemain;
    }
    public int getEnemyRemain(){
        return enemyRemain;
    }
}
