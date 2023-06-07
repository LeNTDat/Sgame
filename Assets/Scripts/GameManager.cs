using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool isSubmitPlayerSize = false;
    [SerializeField] bool isEndGame = false;
    [SerializeField] bool isEndTurn = true;
    [SerializeField] bool isRolled = false;
    [SerializeField] bool isStart = false;
    [SerializeField] int playerSize;

    public bool IsEndGame { get {  return isEndGame; } set { isEndGame = value; } }
    public bool IsEndTurn { get { return isEndTurn; } set { isEndTurn = value; } }
    public bool IsRoll { get { return isRolled; } set { isRolled = value; } }
    public bool IsSumit { get { return isSubmitPlayerSize; }}
    public int PlayerSize { get { return playerSize; } }
    public bool IsStart { get { return isStart; } }

    Dropdown m_Dropdown;
    void Start()
    {
        m_Dropdown = GameObject.FindWithTag("SizePlayer").GetComponentInChildren<Dropdown>();
        PlayerNumber(m_Dropdown);
        m_Dropdown.onValueChanged.AddListener(delegate {
            PlayerNumber(m_Dropdown);
        });
    }

    public void GameStarter ()
    {
        isStart = true;
    }
   
    public void EndTurn()
    {
        isEndTurn = true;
    }

    public void SubmitPlayerSize ()
    {
        isSubmitPlayerSize = true;
    }

    void PlayerNumber(Dropdown change)
    {
        int index = change.value;
        if (!isSubmitPlayerSize)
        {
            playerSize = Int32.Parse(m_Dropdown.options[index].text);
        }
    }

}
