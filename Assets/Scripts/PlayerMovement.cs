using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Progress;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] int currentPosValue;
    [SerializeField] int penaltyPadNum = 3;
    [SerializeField] float waitingTime = 1f;
    [SerializeField] float delayTime = 2f;
    [SerializeField] bool isTurned = false;
    [SerializeField] bool isFinish = false;
    [SerializeField] bool isHavePlayer = true;
    SpanwPlayer players;
    GameManager manager;
    Road road;
    Dice dice;
    public bool Turned { get { return isTurned; } }
    public int CurrentPosValue{ get { return currentPosValue; } }

    void Start()
    {
        manager = GameObject.FindObjectOfType<GameManager>();
        players = GameObject.FindObjectOfType<SpanwPlayer>();
        dice = GameObject.FindAnyObjectByType<Dice>();
        road = GetComponent<Road>();
    }

    void Update()
    {
        if (manager.IsRoll)
        {
            RotatePlayerTurn();
        }
        if (manager.IsEndTurn)
        {
            RestartPlayerTurn();
        }
        

    }

    void OnCheckingPlayer()
    {
        int count = 0;
        int finishPlayer = players.PlayerInPool.Length;
        foreach (var item in players.PlayerInPool)
        {
            if(item.GetComponent<PlayerMovement>().isFinish) { finishPlayer--; }
            if (!item.GetComponent<PlayerMovement>().isFinish)
            {
                if (!item.GetComponent<PlayerMovement>().isTurned)
                {
                    count++;
                }
            }
        }
        if (finishPlayer == 0) { manager.IsEndGame = true; }
        if(count > 0)
        {
            isHavePlayer = false;
        }else if(count == 0 && !isFinish)
        {
            isHavePlayer = true;
        }
    }
    void RestartPlayerTurn()
    {
        OnCheckingPlayer();
        if (isHavePlayer)
        {
            for (int i = players.PlayerInPool.Length - 1; i >= 0; i--)
            {
                PlayerMovement playerIndex = players.PlayerInPool[i].GetComponent<PlayerMovement>();
                if (playerIndex.currentPosValue == road.RoadLine.Count - 1)
                {
                    playerIndex.isFinish = true;
                    playerIndex.isTurned = true;
                }
                playerIndex.isTurned = false;
            }
        }
    }

    void RotatePlayerTurn()
    {
        for(int i = 0; i < players.PlayerInPool.Length; i++)
        {
            PlayerMovement playerIndex = players.PlayerInPool[i].GetComponent<PlayerMovement>();
            
            if (!playerIndex.isTurned && !playerIndex.isFinish)
            {
                StartCoroutine(PlayerMove(i));
                playerIndex.isTurned = true;
                manager.IsRoll = false;
                manager.IsEndTurn = false;
                break;
            }
        }

    }

    IEnumerator PlayerMove(int indexOfPlayerTurn)
    {
        PlayerMovement playerIndex = players.PlayerInPool[indexOfPlayerTurn].GetComponent<PlayerMovement>();
        int IndexPos = playerIndex.CurrentPosValue;
        int padNum = IndexPos + dice.DiceR;
        for (int i = IndexPos + 1; i <= padNum && i < road.RoadLine.Count; i++)
        {
            yield return new WaitForSeconds(waitingTime);
            Vector3 NextPos = road.RoadLine[i].transform.position;
            NextPos.y = NextPos.y + transform.localScale.y / 2 + road.RoadLine[i].transform.localScale.y;
            playerIndex.transform.position = NextPos;
            playerIndex.currentPosValue++;

            if (road.RoadLine[i].isBonus && i == padNum) { 
                yield return new WaitForSeconds(delayTime);
                playerIndex.isTurned = false;
                manager.IsRoll = false;
                manager.IsEndTurn = true;
            }

            if (road.RoadLine[i].isTrap && i == padNum)
            {
                IndexPos = padNum - penaltyPadNum;
                yield return new WaitForSeconds(delayTime);
                Vector3 PenaltyPos = road.RoadLine[IndexPos].transform.position;
                PenaltyPos.y = PenaltyPos.y + transform.localScale.y / 2 + road.RoadLine[IndexPos].transform.localScale.y;
                playerIndex.transform.position = PenaltyPos;
                playerIndex.currentPosValue = IndexPos;
            }
        }
        yield return null;
    }
}
