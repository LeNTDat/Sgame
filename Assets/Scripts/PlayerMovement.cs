using System.Collections;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] int currentPosValue;
    [SerializeField]float waitingTime = 1f;
    [SerializeField] bool isTurned = false; 
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
                players.PlayerInPool[i].GetComponent<PlayerMovement>().Turned = true;
                StartCoroutine(PlayerMove(i));
                manager.IsRoll = false;
                manager.IsEndTurn = false;
                break;
            }
        }
    }
    IEnumerator PlayerMove(int indexOfPlayerTurn)
    {
        int IndexPos = players.PlayerInPool[indexOfPlayerTurn].GetComponent<PlayerMovement>().CurrentPosValue;
        int padNum = IndexPos + dice.DiceR;
       
            for (int i = IndexPos + 1; i <= padNum && i < road.roadLine.Count; i++)
            {
                Vector3 newNextPos = road.roadLine[i].transform.position;
                newNextPos.y = newNextPos.y + transform.localScale.y / 2 + road.roadLine[i].transform.localScale.y;
                yield return new WaitForSeconds(waitingTime);
                players.PlayerInPool[indexOfPlayerTurn].transform.position = newNextPos;
                players.PlayerInPool[indexOfPlayerTurn].GetComponent<PlayerMovement>().CurrentPosValue++;
            }
        yield return null;
    }
}
