using UnityEngine;

public class SpanwPlayer : MonoBehaviour
{
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject[] player;
    [SerializeField] int sizeOfPool;
    GameManager gameManager;
    GameObject[] playerPool;
    bool isSpawned = false;
    public GameObject[] PlayerInPool { get { return playerPool; } }

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (gameManager.isStart && !isSpawned )
        {
            sizeOfPool = gameManager.PlayerSize;
            SpawnPlayerInPool();
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
                playerPool[i].name = i.ToString();
            }
        }
        isSpawned = true;
    }
}
