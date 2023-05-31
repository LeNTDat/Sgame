using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool isRolled = false;
    [SerializeField] bool isEndTurn = true;
    [SerializeField] bool isEndGame = false;
    [SerializeField] int playerSize;
    Dropdown m_Dropdown;
    public bool isStart = false;
    public bool IsRoll { get { return isRolled; } set { isRolled = value; } }
    public bool IsEndTurn { get { return isEndTurn; } set { isEndTurn = value; } }
    public bool IsEndGame { get {  return isEndGame; } set { isEndGame = value; } } 
    public int PlayerSize { get { return playerSize; } }
    void Start()
    {
        m_Dropdown = GameObject.FindWithTag("SizePlayer").GetComponentInChildren<Dropdown>();
        PlayerNumber(m_Dropdown);
        m_Dropdown.onValueChanged.AddListener(delegate {
            PlayerNumber(m_Dropdown);
        });
    }

    public void EndTurn()
    {
        IsEndTurn = true;
    }

    void PlayerNumber(Dropdown change)
    {
        int index = change.value;
        if (!isStart)
        {
            playerSize = Int32.Parse(m_Dropdown.options[index].text);
        }
    }

}
