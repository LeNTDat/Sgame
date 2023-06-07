using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] int currentPosValue;
    [SerializeField] int turn = 0;
    [SerializeField] int bonus = 0;
    [SerializeField] int trap = 0;
    [SerializeField] int penaltyPadNum = 3;
    [SerializeField] float waitingTime = 0.3f;
    [SerializeField] float delayTime = 0.5f;
    [SerializeField] bool isTurned = false;
    [SerializeField] bool isFinish = false;
    [SerializeField] bool isCanMove = true;
    [SerializeField] bool isHavePlayer = true;

    ScoreManager scoreManager;
    SpanwPlayer players;
    GameManager manager;
    UiManager uiManager;
    Road road;
    Dice dice;

    public int NumberOfTurn{ get { return turn; } }
    public int NumberOfTrap { get { return trap; } }
    public int NumberOfBonus { get { return bonus; } }



    void Start()
    {
        uiManager = GameObject.FindObjectOfType<UiManager>();
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        manager = GameObject.FindObjectOfType<GameManager>();
        players = GameObject.FindObjectOfType<SpanwPlayer>();
        dice = GameObject.FindAnyObjectByType<Dice>();
        road = GetComponent<Road>();
    }

    void Update()
    {
        RotatePlayerTurn();
        if (manager.IsEndTurn && !manager.IsEndGame)
        {
            RestartPlayerTurn();
            FinishCheck();
        }
    }

    void OnCheckingPlayer()
    {
        int count = 0;
        int finishPlayer = players.PlayerOutPool.Count;
        foreach (var item in players.PlayerOutPool)
        {
            PlayerMovement playersIndex = item.GetComponent<PlayerMovement>();
            if (playersIndex.currentPosValue == road.RoadLine.Count - 1)
            {
                playersIndex.isTurned = true;
                playersIndex.isFinish = true;
                playersIndex.isCanMove = false;
                break;
            }
            if (playersIndex.isFinish) { 
                finishPlayer--;
            }
            if (!playersIndex.isFinish)
            {
                if (!playersIndex.isTurned)
                {
                    count++;
                }
            }
        }
        if (finishPlayer == 0) { manager.IsEndGame = true;  }
        if(count > 0){ isHavePlayer = false;}
        else if(count == 0 ){isHavePlayer = true;}
    }

    void RestartPlayerTurn()
    {
        OnCheckingPlayer();
        for (int i = players.PlayerOutPool.Count - 1; i >= 0; i--)
        {
            PlayerMovement playerIndex = players.PlayerOutPool[i].GetComponent<PlayerMovement>();
            
            if (isHavePlayer)
            {
                playerIndex.isTurned = false;
            }
        }
    }
 
    void RotatePlayerTurn()
    {
        if (players.PlayerOutPool.Count == 1 || players.PlayerOutPool.Count == 0)
        {
            manager.IsEndTurn = true;
        }
        if (manager.IsRoll)
        {
            for (int i = 0; i < players.PlayerOutPool.Count; i++)
            {
                uiManager.ShowTurnName(players.PlayerOutPool[i].name);
                PlayerMovement playerIndex = players.PlayerOutPool[i].GetComponent<PlayerMovement>();
                
                if (!playerIndex.isTurned && !playerIndex.isFinish)
                {
                    StartCoroutine(PlayerMove(i));
                    break;
                }
            }
        }
    }

    void FinishCheck()
    {
        for (int i = 0; i < players.PlayerOutPool.Count; i++)
        {
            if (players.PlayerOutPool[i].GetComponent<PlayerMovement>().isFinish)
            {
                scoreManager.GetFinishList(players.PlayerOutPool[i], players.PlayerOutPool[i].transform.position);
                Destroy(players.PlayerOutPool[i],0.5f);
                players.PlayerOutPool.RemoveAt(i);
                break;
            }
        }
    }

    void GetPenaltyTrap(PlayerMovement playerIndex, int penaltyPos)
    {
        Vector3 PenaltyPos = road.RoadLine[penaltyPos].transform.position;
        PenaltyPos.y = PenaltyPos.y + transform.localScale.y / 2 + road.RoadLine[penaltyPos].transform.localScale.y;
        playerIndex.transform.position = PenaltyPos;
        playerIndex.currentPosValue = penaltyPos;
        playerIndex.trap++;
    }

    void GetBonusTurn(PlayerMovement playerIndex)
    {
        playerIndex.isTurned = false;
        manager.IsRoll = false;
        manager.IsEndTurn = true;
        playerIndex.bonus++;
    }

    IEnumerator PlayerMove(int indexOfPlayerTurn)
    {
        PlayerMovement playerIndex = players.PlayerOutPool[indexOfPlayerTurn].GetComponent<PlayerMovement>();
        int IndexPos = playerIndex.currentPosValue;
        int padNum = IndexPos + dice.DiceR;
        playerIndex.isTurned = true;
        manager.IsEndTurn = false;
        manager.IsRoll = false;
        if (isCanMove)
        {
            for (int i = IndexPos + 1; i <= padNum && i < road.RoadLine.Count ; i++)
            {
                yield return new WaitForSeconds(waitingTime);
                Vector3 NextPos = road.RoadLine[i].transform.position;
                NextPos.y = NextPos.y + transform.localScale.y / 2 + road.RoadLine[i].transform.localScale.y;
                playerIndex.transform.position = NextPos;
                playerIndex.currentPosValue++;

                if (road.RoadLine[i].isBonus && i == padNum) { 
                    yield return new WaitForSeconds(delayTime);
                    GetBonusTurn(playerIndex);
                }

                if (road.RoadLine[i].isTrap && i == padNum)
                {
                    IndexPos = padNum - penaltyPadNum;
                    yield return new WaitForSeconds(delayTime);
                    GetPenaltyTrap(playerIndex, IndexPos);
                    if (road.RoadLine[IndexPos].isBonus)
                    {
                        yield return new WaitForSeconds(delayTime);
                        GetBonusTurn(playerIndex);
                    }
                    if (road.RoadLine[IndexPos].isTrap)
                    {
                        IndexPos -= penaltyPadNum;
                        yield return new WaitForSeconds(delayTime);
                        GetPenaltyTrap(playerIndex, IndexPos);
                        if (road.RoadLine[IndexPos].isBonus)
                        {
                            yield return new WaitForSeconds(delayTime);
                            GetBonusTurn(playerIndex);
                        }
                    }

                }
            }
                playerIndex.turn++;
        }
       
        yield return null;
    }
}
