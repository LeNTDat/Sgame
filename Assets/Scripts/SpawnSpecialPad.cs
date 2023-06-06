using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class SpawnSpecialPad : MonoBehaviour
{
    [SerializeField] int minPad = 3;
    [SerializeField] int maxPad = 24;
    [SerializeField] List<Point> Points;
    bool rolling = false;
    GameObject road;

    void Start()
    {
        road = GameObject.FindGameObjectWithTag("MovePad");
        GetRoadPad();
        FindSpecialPad();
    }

    void GetRoadPad()
    {
        Points = new List<Point>();
        foreach (Transform item in road.transform)
        {
            Points.Add(item.gameObject.GetComponent<Point>());
        }
    }

    void FindSpecialPad() {
        for (int i = 0; i < 6; i++)
        {
            int randomNum = Random.Range(minPad, maxPad);
            if (rolling && (!Points[randomNum].isBonus || !Points[randomNum].isTrap) )
            {
                DisplayBonusPad(randomNum);
                rolling = false;
            }
            else if(!rolling && (!Points[randomNum].isBonus || !Points[randomNum].isTrap))
            {
                DisplayTrapPad(randomNum);
                rolling = true;
            }
        }

    }

    void DisplayBonusPad(int indexBonus)
    {
        Points[indexBonus].isBonus = true;
        Points[indexBonus].GetComponent<Renderer>().material.color = Color.green;
    }
    void DisplayTrapPad(int indexTrap)
    {
        Points[indexTrap].isTrap = true;
        Points[indexTrap].GetComponent<Renderer>().material.color = Color.red;
    }
}
