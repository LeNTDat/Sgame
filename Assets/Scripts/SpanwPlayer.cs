using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SpanwPlayer : MonoBehaviour
{
    [SerializeField] TMP_InputField[] inputField;
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject[] player;
    [SerializeField] string[] playerName;
    [SerializeField] int sizeOfPool;

    GameManager gameManager;
    List<GameObject> gameObjList;

    public List<GameObject> PlayerOutPool { get { return gameObjList; } }
    bool isSpawned = false;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        SpawnInputName();

        if (gameManager.isStart && !isSpawned )
        {
            GetPlayerName();
            SpawnPlayerInPool();
        }
    }

    void SpawnInputName()
    {
        if (gameManager.IsSumit){
        sizeOfPool = gameManager.PlayerSize;
            for ( int i = 0; i < sizeOfPool; i++)
            {
                inputField[i].gameObject.SetActive(true);
            }
        }
    }

    void GetPlayerName()
    {
        playerName = new string[sizeOfPool];
        for( int i = 0; i < sizeOfPool; i++ )
        {
            if(playerName[i] == null)
            {
                if(inputField[i].text == null || inputField[i].text == "")
                {
                    playerName[i] = "NoName_" + i;
                }
                else
                {
                    playerName[i] = inputField[i].text;
                }
            }
        }
    }

    void SpawnPlayerInPool ()
    {
        gameObjList = new List<GameObject>();
        for (int i = 0; i < sizeOfPool; i++)
        {
            Vector3 StartPos = startPoint.transform.position;
            StartPos.y = StartPos.y + player[i].transform.localScale.y / 2 + startPoint.transform.localScale.y;
            GameObject obj = Instantiate(player[i], StartPos, Quaternion.identity);
            gameObjList.Add(obj);
            gameObjList[i].name = playerName[i];
        }
        isSpawned = true;
    }
}
