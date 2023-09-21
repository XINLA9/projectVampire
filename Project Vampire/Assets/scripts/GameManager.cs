using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] hunterPrefabs;
    public GameObject[] monsterPrefabs;
    public int[] monsterRemaining = new int[] { 5, 1, 1 };
    public int[] spawnNum = new int[] { 1, 1, 1 };
    public List<int[]> difficultySpawn = new List<int[]>{
                new int[] {3, 2},
                new int[] {4, 3},
                new int[] {5, 4}
            };
    int difficulty;
    private float spawnRangeX = 5;
    private float spawnRangeZ = 5;
    private int monsterNo = 0;
    private bool isGameActive = false;

    // attribute for UI 
    public Button restartButton;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI loseText;
    public GameObject titleScreen;
    public GameObject gameScreen;
    public TextMeshProUGUI numText1;
    public TextMeshProUGUI numText2;
    public TextMeshProUGUI numText3;


    // Start is called before the first frame update
    void Start()
    {

    }
    // begin the game
    public void StartGame(int difficulty)
    {
        this.difficulty = difficulty;
        titleScreen.gameObject.SetActive(false);
        gameScreen.gameObject.SetActive(true);
        SpawnEnemy();
        isGameActive = true;
    }
    // Generate random spawn position for hunters on the map
    Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnRangeZ, -spawnRangeZ);
        return new Vector3(xPos, 1, zPos);
    }
    // generate the hunters for this game
    void SpawnEnemy()
    {
        // Spawn number of swordman
        for (int i = 0; i < difficultySpawn[difficulty][0]; i++)
        {
            Instantiate(hunterPrefabs[0], GenerateSpawnPosition(), hunterPrefabs[0].transform.rotation);
        }
        // spawn number of ranger
        for (int i = 0; i < difficultySpawn[difficulty][1]; i++)
        {
            Instantiate(hunterPrefabs[1], GenerateSpawnPosition(), hunterPrefabs[1].transform.rotation);
        }
        
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
        GameEnd();
        numText1.text = "wolf:" + monsterRemaining[0];
        numText2.text = "horse:" + monsterRemaining[1];
        numText3.text = "mist:" + monsterRemaining[2];
    }
    bool IsInAllowedRange(Vector3 position)
    {
        // Define the location of the center of the map
        Vector3 mapCenter = new Vector3(0f, 2f, 0f);
        // Define the boundaries of the peripheral area (outside the 15x15 square)
        float outerBoundary = 15f;
        // Define the boundaries of the internal area (within a 25x25 square)
        float innerBoundary = 25f;
        // Calculate the distance from the generated location to the center of the map
        float distanceToCenter = Vector3.Distance(position, mapCenter);
        // Check if the generated location is within the specified range
        return distanceToCenter > outerBoundary && distanceToCenter <= innerBoundary;
    }

    void SpawnMonsters(int monsterNO, Vector3 position)
    {
        if (monsterRemaining[monsterNO] > 0)
        {
            for (int i = 0; i < spawnNum[monsterNo]; i++)
            {
                Instantiate(monsterPrefabs[monsterNO], position, monsterPrefabs[monsterNO].transform.rotation);
            }
            monsterRemaining[monsterNO]--;
        }
    }
    public void SetMonster(int monsterNo)
    {
        this.monsterNo = monsterNo;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameEnd()
    {
        if (isGameActive)
        {
            int hunterNumber = GameObject.FindGameObjectsWithTag("hunter").Length;
            int monsterNumber = GameObject.FindGameObjectsWithTag("monster").Length;
            int totalMonsterRemaining = 0;
            foreach (int remaining in monsterRemaining)
            {
                totalMonsterRemaining += remaining;
            }
            if (hunterNumber == 0)
            {
                restartButton.gameObject.SetActive(true);
                winText.gameObject.SetActive(true);
            }
            if(totalMonsterRemaining == 0 && monsterNumber == 0)
            {
                restartButton.gameObject.SetActive(true);
                loseText.gameObject.SetActive(true);
            }

        }
    }
}
