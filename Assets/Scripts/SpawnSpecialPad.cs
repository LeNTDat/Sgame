using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpecialPad : MonoBehaviour
{
    [SerializeField] int minPad = 3;
    [SerializeField] int maxPad = 24;
    [SerializeField] List<Point> Points;
    GameObject road;

    void Start()
    {
        road = GameObject.FindGameObjectWithTag("MovePad");
        FindSpecialPad();
    }

    void FindSpecialPad() { 
        int RandTrap = Random.Range(minPad, maxPad);
        int RandBonus = Random.Range(minPad, maxPad);
        if (RandTrap == RandBonus) { RandBonus++; }
        Points = new List<Point>();
        foreach (Transform item in road.transform)
        {
            Points.Add(item.gameObject.GetComponent<Point>());
        }
        DisplayBonusPad(RandBonus);
        DisplayTrapPad(RandTrap);

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
