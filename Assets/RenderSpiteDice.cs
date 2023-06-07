using UnityEngine;
using UnityEngine.UI;

public class RenderSpiteDice : MonoBehaviour
{
    [SerializeField] Sprite[] listOfDice;
    Image image;
    Dice dice;
    void Start()
    {
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
