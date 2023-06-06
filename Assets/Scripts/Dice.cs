using UnityEngine;

public class Dice : MonoBehaviour
{
    GameManager gameManager;
    int diceRolled = 1;

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
    }
}
