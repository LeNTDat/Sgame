using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] int currentPosValue;
    [SerializeField] float waitingTime = 1f;
    [SerializeField] bool isTurned = false;
    [SerializeField] int penaltyPadNum = 3;
    SpanwPlayer players;
    GameManager manager;
    Road road;
    Dice dice;
    public bool Turned { get { return isTurned; } set { isTurned = value; } }
    public int CurrentPosValue{ get { return currentPosValue; } set { currentPosValue = value; } }


    void Start()
    {
        manager = GameObject.FindObjectOfType<GameManager>();
        manager.IsTurn = isTurned;
        players = GameObject.FindObjectOfType<SpanwPlayer>();
        dice = GameObject.FindAnyObjectByType<Dice>();
        road = GetComponent<Road>();
    }

    void Update()
    {
        if (manager.IsRoll)
        {
            RotatePlayerTurn();
            RestartPlayerTurn();
        }

    }
    void RestartPlayerTurn()
    {
        for (int i = 0; i < players.PlayerInPool.Length; i++)
        {
            if (players.PlayerInPool.Last().GetComponent<PlayerMovement>().Turned == true)
            {
                players.PlayerInPool[i].GetComponent<PlayerMovement>().Turned = false;
            }
        }
    }

    void RotatePlayerTurn()
    {
        for(int i = 0; i < players.PlayerInPool.Length; i++)
        {
            if (!players.PlayerInPool[i].GetComponent<PlayerMovement>().Turned)
            {
                StartCoroutine(PlayerMove(i));
                players.PlayerInPool[i].GetComponent<PlayerMovement>().Turned = true;
                manager.IsRoll = false;
                manager.IsEndTurn = false;
                break;
            }
        }
    }

    void PlayerGetMoving(Vector3 pos,int indexOfPlayerTurn, int indexOfPad)
    {
        pos.y = pos.y + transform.localScale.y / 2 + road.RoadLine[indexOfPad].transform.localScale.y;
        players.PlayerInPool[indexOfPlayerTurn].transform.position = pos;
    }

    IEnumerator PlayerMove(int indexOfPlayerTurn)
    {
        int IndexPos = players.PlayerInPool[indexOfPlayerTurn].GetComponent<PlayerMovement>().CurrentPosValue;
        int padNum = IndexPos + dice.DiceR;
       
            for (int i = IndexPos + 1; i <= padNum && i < road.RoadLine.Count; i++)
            {
                yield return new WaitForSeconds(waitingTime);
                Vector3 newNextPos = road.RoadLine[i].transform.position;
                PlayerGetMoving(newNextPos,indexOfPlayerTurn, i);
                players.PlayerInPool[indexOfPlayerTurn].GetComponent<PlayerMovement>().CurrentPosValue++;

                if (road.RoadLine[i].isBonus && i == padNum) { 
                    yield return new WaitForSeconds(waitingTime);
                    players.PlayerInPool[indexOfPlayerTurn].GetComponent<PlayerMovement>().Turned = false;
                    manager.IsRoll = false;
                    manager.IsEndTurn = false;
                }
                if (road.RoadLine[i].isTrap && i == padNum)
                {
                    IndexPos = padNum - penaltyPadNum;
                    yield return new WaitForSeconds(waitingTime);
                    Vector3 newPenaltyPos = road.RoadLine[IndexPos].transform.position;
                    PlayerGetMoving(newPenaltyPos, indexOfPlayerTurn, IndexPos);
                    players.PlayerInPool[indexOfPlayerTurn].GetComponent<PlayerMovement>().CurrentPosValue = IndexPos;

            }
        }
        yield return null;
    }
}
