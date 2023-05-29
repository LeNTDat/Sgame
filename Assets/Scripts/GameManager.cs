using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool isPlayerTurn = false;
    [SerializeField] bool isRolled = false;
    [SerializeField] bool isEndTurn = true;
    [SerializeField] int playerSize;
    Dropdown m_Dropdown;
    public bool isStart = false;
    public bool IsTurn { get { return isPlayerTurn; } set { isPlayerTurn = value; } }
    public bool IsRoll { get { return isRolled; } set { isRolled = value; } }
    public bool IsEndTurn { get { return isEndTurn; } set { isEndTurn = value; } }
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

    public void PlayerNumber(Dropdown change)
    {
        int index = change.value;
        if (!isStart)
        {
            playerSize = Int32.Parse(m_Dropdown.options[index].text);
        }
    }

}
