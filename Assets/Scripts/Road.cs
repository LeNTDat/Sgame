using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] List<Point> roads;
    public List<Point> RoadLine{ get { return roads; } }
    void Start()
    {
        FindingRoads();
    }

    void FindingRoads()
    {
        roads = new List<Point>();
        GameObject parent = GameObject.FindGameObjectWithTag("MovePad");
        foreach (Transform item in parent.transform)
        {
            roads.Add(item.gameObject.GetComponent<Point>());
        }
    }
}
