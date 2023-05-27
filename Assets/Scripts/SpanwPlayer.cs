using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpanwPlayer : MonoBehaviour
{
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject player;
    [SerializeField] int sizeOfPool = 2;
    GameObject[] playerPool;
    public GameObject[] PlayerInPool { get { return playerPool; } }

    void Start()
    {
        SpawnPlayerInPool();
    }

    void SpawnPlayerInPool ()
    {
        playerPool = new GameObject[sizeOfPool];
        for (int i = 0; i < sizeOfPool; i++)
        {
            Vector3 StartPos = startPoint.transform.position;
            StartPos.y = StartPos.y + player.transform.localScale.y / 2 + startPoint.transform.localScale.y;
            if (playerPool[i] == null)
            {
                playerPool[i] = Instantiate(player, StartPos, Quaternion.identity);
                playerPool[i].name = i.ToString();
            }
        }
    }

   
}
