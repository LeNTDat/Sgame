using UnityEngine;
using UnityEngine.UI;

public class RenderSpiteDice : MonoBehaviour
{
    [SerializeField] Sprite[] listOfDice;
    GameManager gameManager;
    Image image;
    Dice dice;
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        dice = GameObject.FindObjectOfType<Dice>();
        image = GetComponent<Image>();
    }

    void Update()
    {
            DisplayDice(dice.DiceR); 
    }

    public void DisplayDice(int val)
    {
        image.sprite = listOfDice[val - 1];
    }
}
