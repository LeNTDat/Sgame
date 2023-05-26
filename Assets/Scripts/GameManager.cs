using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isPlayerTurn = false;
    public bool isRolled = false;
    public bool isStart = false;
    public static GameManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

}
