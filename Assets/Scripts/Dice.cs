using TMPro;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] TMP_Text diceVal;
    GameManager gameManager;
    int diceRolled;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public int DiceR { get { return diceRolled; }}

    public void RollDice()
    {
        if (gameManager.IsEndTurn) { 
            diceRolled = Random.Range(1, 7);
            gameManager.IsRoll = true;
        }
        DisplayRollValue();
    }

    public void DisplayRollValue()
    {
        diceVal.text = diceRolled.ToString();
    }
}
