using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] int currentPosValue;
    [SerializeField]float waitingTime = 1f;
    public bool isTurned = false; 
    SpanwPlayer players;
    Road road;
    Dice dice;



    void Start()
    {
        GameManager.instance.isPlayerTurn = isTurned;
        dice = GameObject.FindAnyObjectByType<Dice>();
        players = GameObject.FindObjectOfType<SpanwPlayer>();
        road = GetComponent<Road>();
    }

    void Update()
    {
        if (GameManager.instance.isRolled)
        {
            RotatePlayerTurn();
            RestartPlayerTurn();
        }

    }
    void RestartPlayerTurn()
    {
        for (int i = 0; i < players.PlayerInPool.Length; i++)
        {
            if (players.PlayerInPool.Last().GetComponent<PlayerMovement>().isTurned == true)
            {
                players.PlayerInPool[i].GetComponent<PlayerMovement>().isTurned = false;
            }
        }
    }

    void RotatePlayerTurn()
    {
        for(int i = 0; i < players.PlayerInPool.Length; i++)
        {
           
            if (!isTurned)
            {
                StartCoroutine(PlayerMove());
                GameManager.instance.isRolled = false;
                isTurned = true;
                break;
            }
        }
    }
    IEnumerator PlayerMove()
    {
        int padNum = currentPosValue + dice.dice;
       
            for (int i = currentPosValue + 1; i <= padNum && i < road.roadLine.Count; i++)
            {
                if (i < 1)
                {
                    continue;
                }
                Vector3 newNextPos = road.roadLine[i].transform.position;
                newNextPos.y = newNextPos.y + transform.localScale.y / 2 + road.roadLine[i].transform.localScale.y;
                yield return new WaitForSeconds(waitingTime);
                transform.position = newNextPos;
                currentPosValue++;
            }  
        yield return null;
    }
}
