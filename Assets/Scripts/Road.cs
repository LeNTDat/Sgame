using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] List<Point> roads = new List<Point>();
    [SerializeField] int diceVal;
    [SerializeField] int currentPosValue;
    Dice dice;

    void Start()
    {
        dice = GameObject.FindAnyObjectByType<Dice>();
        FindingRoads();
        setStartPoint();
    }

    void Update()
    {
        if (GameManager.instance.isRolled)
        {
            PlayerMovement();
            GameManager.instance.isRolled = false;
        }
    }
    void FindingRoads()
    {
        GameObject parent = GameObject.FindGameObjectWithTag("MovePad");
        foreach (Transform item in parent.transform)
        {
            roads.Add(item.gameObject.GetComponent<Point>());
        }
    }

    void setStartPoint()
    {
        Vector3 StartPos = roads.First().transform.position;
        StartPos.y = StartPos.y + Player.transform.localScale.y/2 + roads.First().transform.localScale.y;
        Player.transform.position = StartPos;
        currentPosValue = 0;
    }

    public void PlayerMovement()
    {
        int padNum = currentPosValue + dice.dice;
        if( padNum > roads.Count ) { 
            padNum = roads.Count;
        }
        for (int i = currentPosValue; i <= padNum; i++)
        {
            if (currentPosValue < padNum)
            {
                currentPosValue++;
            }
            else
            {
                Player.transform.position = roads[i].transform.position;
            }
        }
    }
}
