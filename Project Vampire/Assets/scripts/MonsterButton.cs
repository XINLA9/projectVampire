using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterButton : MonoBehaviour
{
    private Button button;
    private GameManager gameManager;
    public int monsterNo;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        button.onClick.AddListener(SetMonster);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetMonster()
    {
        Debug.Log("click on " + gameObject.name);
        gameManager.SetMonster(monsterNo);
    }
    
}
