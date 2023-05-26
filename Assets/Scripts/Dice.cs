using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] TMP_Text diceVal;
    int diceRolled;

    public int dice { get { return diceRolled; }}

    public void RollDice()
    {
        diceRolled = Random.Range(1, 7);
        GameManager.instance.isRolled = true;
        DisplayRollValue();
    }

    public void DisplayRollValue()
    {
        diceVal.text = diceRolled.ToString();
    }
}
