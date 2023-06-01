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
    GameObject[] playerPool;

    public GameObject[] PlayerInPool { get { return playerPool; } }
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
                playerName[i] = inputField[i].text;
            }
        }
    }

    void SpawnPlayerInPool ()
    {
        playerPool = new GameObject[sizeOfPool];
        for (int i = 0; i < sizeOfPool; i++)
        {
            Vector3 StartPos = startPoint.transform.position;
            StartPos.y = StartPos.y + player[i].transform.localScale.y / 2 + startPoint.transform.localScale.y;
            if (playerPool[i] == null)
            {
                playerPool[i] = Instantiate(player[i], StartPos, Quaternion.identity);
                playerPool[i].name = playerName[i];
            }
        }
        isSpawned = true;
    }
}
