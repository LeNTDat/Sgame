using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Road : MonoBehaviour
{
    GameObject Player;
    [SerializeField] List<Point> roads = new List<Point>();

    public List<Point> roadLine{ get { return roads; } }
    void Start()
    {
        
        FindingRoads();
        
    }
    void FindingRoads()
    {
        GameObject parent = GameObject.FindGameObjectWithTag("MovePad");
        foreach (Transform item in parent.transform)
        {
            roads.Add(item.gameObject.GetComponent<Point>());
        }
    }


    
}
